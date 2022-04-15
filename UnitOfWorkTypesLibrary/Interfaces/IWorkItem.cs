﻿namespace UnitOfWorkTypesLibrary
{
    using Microsoft.EntityFrameworkCore;

    public enum WorkItemResultEnum
    {
        notSet,

        doneContinue,

        cancelWithoutError,

        rollbackExit,

        commitExit,

        commitSuccessfullyCompleted,

        deadlockError,

        concurrencyError,

        exceptionError
    }

    public interface IWorkItem
    {
        WorkItemResultEnum Execute(DbContext context);
    }
}