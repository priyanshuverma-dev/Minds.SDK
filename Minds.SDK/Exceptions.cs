using System;

namespace Minds.SDK
{
    /// <summary>
    /// Represents an exception that is thrown when a requested object is not found.
    /// </summary>
    public class ObjectNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotFoundException"/> class.
        /// </summary>
        public ObjectNotFoundException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ObjectNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ObjectNotFoundException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Represents an exception that is thrown when an unsupported object operation is attempted.
    /// </summary>
    public class ObjectNotSupportedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotSupportedException"/> class.
        /// </summary>
        public ObjectNotSupportedException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotSupportedException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ObjectNotSupportedException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotSupportedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ObjectNotSupportedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Represents an exception that is thrown when a request is forbidden due to lack of access permissions.
    /// </summary>
    public class ForbiddenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        public ForbiddenException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ForbiddenException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ForbiddenException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Represents an exception that is thrown when a request is unauthorized.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        public UnauthorizedException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UnauthorizedException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public UnauthorizedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Represents an exception that is thrown when an unknown error occurs during an API request.
    /// </summary>
    public class UnknownErrorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownErrorException"/> class.
        /// </summary>
        public UnknownErrorException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownErrorException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UnknownErrorException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownErrorException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public UnknownErrorException(string message, Exception inner) : base(message, inner) { }
    }
}
