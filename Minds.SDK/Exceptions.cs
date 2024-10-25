using System;

namespace Minds.SDK {
    public class ObjectNotFoundException : Exception {
        public ObjectNotFoundException() : base() { }

        public ObjectNotFoundException(string message) : base(message) { }

        public ObjectNotFoundException(string message, Exception inner) : base(message, inner) { }
    }

    public class ObjectNotSupportedException : Exception {
        public ObjectNotSupportedException() : base() { }

        public ObjectNotSupportedException(string message) : base(message) { }

        public ObjectNotSupportedException(string message, Exception inner) : base(message, inner) { }
    }

    public class ForbiddenException : Exception {
        public ForbiddenException() : base() { }

        public ForbiddenException(string message) : base(message) { }

        public ForbiddenException(string message, Exception inner) : base(message, inner) { }
    }

    public class UnauthorizedException : Exception {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception inner) : base(message, inner) { }
    }

    public class UnknownErrorException : Exception {
        public UnknownErrorException() : base() { }

        public UnknownErrorException(string message) : base(message) { }

        public UnknownErrorException(string message, Exception inner) : base(message, inner) { }
    }
}
