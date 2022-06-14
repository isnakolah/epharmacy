﻿namespace EPharmacy.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        (Succeeded, Errors) = (succeeded, errors.ToArray());
    }

    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }

    public static Result Failure(string error)
    {
        var errors = new List<string> { error };

        return new Result(false, errors);
    }
}
