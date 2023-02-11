﻿using System.Reflection;

namespace DatabaseContext
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        private readonly PropertyInfo? createdOnPropertyInfo;
        private readonly PropertyInfo? lastModifiedPropertyInfo;

        public BaseEntity()
        {
            this.createdOnPropertyInfo = this.GetType().GetProperty("CreatedOn");
            this.lastModifiedPropertyInfo = this.GetType().GetProperty("LastModified");
        }

        public void SetCreatedOn(DateTime dateTime)
        {
            this.createdOnPropertyInfo?.SetValue(this, dateTime);
        }

        public void SetLastModified(DateTime dateTime)
        {
            this.lastModifiedPropertyInfo?.SetValue(this, dateTime);
        }
    }
}