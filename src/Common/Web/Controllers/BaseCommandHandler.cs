using Common;
using Common.Exceptions;
using Common.Implementations;
using Common.Interfaces;
using Common.Models;
using DAL;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Web.Controls;

namespace Web.Controllers
{
    public abstract class BaseCommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : BaseCommand<TResult>
    {
        private readonly ICacheProvider CachingCommandTransaction = new GlobalMemoryCacheProvider();


        protected BaseCommandHandler()
        {
        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            //Validation
            Type typeValidation = typeof(TCommand);
            Assembly assembly = Assembly.GetAssembly(typeValidation);
            var validatorBaseType = typeof(AbstractValidator<>).MakeGenericType(typeValidation);
            var validatorType = assembly.GetTypes().FirstOrDefault(type => type.IsSubclassOf(validatorBaseType));
            if (validatorType != null)
            {
                var validator = Activator.CreateInstance(validatorType);
                var method = validatorBaseType.GetMethods().First(m => m.Name == "Validate" && m.GetParameters()[0].ParameterType == typeValidation);
                ValidationResult results = (ValidationResult)method.Invoke(validator, new object[] { request });
                if (!results.IsValid)
                {
                    IEnumerable<ApiResult> returnErrors = from error
                                                          in results.Errors
                                                          select new ApiResult() { Result = -1, ErrorMessage = error.ErrorMessage };

                    throw new BusinessException(JsonConvert.SerializeObject(returnErrors));
                }
            }
            CachingCommandTransactionModel transModel = null;
            switch (request.CmdStyle)
            {
                case CommandStyles.Normal:
                    return await HandleCommand(request, cancellationToken);
                case CommandStyles.Transaction:
                    if (CachingCommandTransaction.TryGetValue(CachingConstant.CACHINGCOMMAND, request.CommandId, out transModel))
                    {
                        return await HandleCommandTransaction(request, transModel, cancellationToken);
                    }

                    var conn = DalHelper.GetConnection();
                    conn.Open();
                    var trans = conn.BeginTransaction();
                    transModel = new CachingCommandTransactionModel()
                    {
                        Connection = conn,
                        Transaction = trans
                    };
                    if (!CachingCommandTransaction.TrySetValue(CachingConstant.CACHINGCOMMAND, request.CommandId, transModel))
                    {
                        throw new BusinessException("Common.CachingData.SetError");
                    }

                    return await HandleCommandTransaction(request, transModel, cancellationToken);

                case CommandStyles.CommitTransaction:
                    if (!CachingCommandTransaction.TryGetValue(CachingConstant.CACHINGCOMMAND, request.CommandId, out transModel))
                    {
                        throw new BusinessException("Common.CachingData.GetError");
                    }

                    transModel.Transaction.Commit();
                    transModel.Transaction.Dispose();
                    transModel.Connection.Dispose();

                    //Remove TransModel
                    CachingCommandTransaction.TryRemoveValue<CachingCommandTransactionModel>(CachingConstant.CACHINGCOMMAND, request.CommandId);
                    return default;
                default:
                    if (!CachingCommandTransaction.TryGetValue(CachingConstant.CACHINGCOMMAND, request.CommandId, out transModel))
                    {
                        throw new BusinessException("Common.CachingData.GetError");
                    }

                    transModel.Transaction.Rollback();
                    transModel.Transaction.Dispose();
                    transModel.Connection.Dispose();

                    //Remove TransModel
                    CachingCommandTransaction.TryRemoveValue<CachingCommandTransactionModel>(CachingConstant.CACHINGCOMMAND, request.CommandId);
                    return default;
            }
        }

        public abstract Task<TResult> HandleCommand(TCommand request, CancellationToken cancellationToken);
        public virtual Task<TResult> HandleCommandTransaction(TCommand request, CachingCommandTransactionModel trans, CancellationToken cancellationToken)
        {
            return default;
        }

        public T CreateBuild<T>(T obj, UserSession userSession) where T : BaseModel
        {
            obj.CreatedDate = DateTime.Now;
            obj.CreatedBy = userSession.UserId;
            obj.IsDeleted = false;
            return obj;
        }

        public T UpdateBuild<T>(T obj, UserSession userSession) where T : BaseModel
        {
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = userSession.UserId;
            return obj;
        }

        public T DeleteBuild<T>(T obj, UserSession userSession) where T : BaseModel
        {
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = userSession.UserId;
            obj.IsDeleted = true;
            return obj;
        }
    }
}
