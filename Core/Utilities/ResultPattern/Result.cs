﻿using Core.Utilities.ResultPattern.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.ResultPattern
{
    public class Result : IResult
    {

        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Message = message;
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }

        public string Message { get; }
    }
}
