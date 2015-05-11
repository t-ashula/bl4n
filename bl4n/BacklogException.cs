// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogException.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;

namespace BL4N
{
    /// <summary> API �̗�O��\���܂� </summary>
    public class BacklogException : Exception
    {
        /// <summary> �G���[�̗��R��\���܂� </summary>
        public enum ErrorReason
        {
            /// <summary> unknown reason </summary>
            Unknown = 0,

            /// <summary> If an error occurs in the API Server. 1 </summary>
            InternalError = 1,

            /// <summary> If you call the API that is not available in your licence. 2 </summary>
            LicenceError = 2,

            /// <summary> If space licence has expired. 3 </summary>
            LicenceExpiredError = 3,

            /// <summary> If you access from the IP Address that is not allowed. 4 </summary>
            AccessDeniedError = 4,

            /// <summary> If your operation is denied. 5 </summary>
            UnauthorizedOperationError = 5,

            /// <summary> If you access the resource that does not exist. 6 </summary>
            NoResourceError = 6,

            /// <summary> If you post a request for invalid parameters. 7 </summary>
            InvalidRequestError = 7,

            /// <summary> If over capacity limit of space. 8 </summary>
            SpaceOverCapacityError = 8,

            /// <summary> If you call API to add a resource when it exceeds a limit provided in the resource. 9 </summary>
            ResourceOverflowError = 9,

            /// <summary> If the uploaded attachment is too large. 10 </summary>
            TooLargeFileError = 10,

            /// <summary> If you are not registered on a target space. 11 </summary>
            AuthenticationError = 11
        }

        /// <summary>
        /// <see cref="BacklogException"/> �̃C���X�^���X�����������܂�
        /// </summary>
        public BacklogException()
        {
            Reasons = new[] { ErrorReason.Unknown };
            ReasonMessages = new[] { string.Empty };
        }

        /// <summary> �G���[��������� <see cref="BacklogException"/> �̃C���X�^���X�����������܂� </summary>
        /// <param name="response">�G���[����</param>
        public BacklogException(BacklogErrorResponse response)
        {
            Reasons = response.Errors.Select(i => (ErrorReason)i.Code).ToArray();
            ReasonMessages = response.Errors.Select(i => i.Message).ToArray();
            StatusCode = response.StatusCode;
        }

        /// <summary> �G���[�̗��R�i�����j���擾���܂� </summary>
        public ErrorReason[] Reasons { get; private set; }

        /// <summary> �G���[�̗��R�𕶎���Ŏ擾���܂� </summary>
        public string[] ReasonMessages { get; private set; }

        /// <summary> HTTP �X�e�[�^�X�R�[�h���擾���܂� </summary>
        public HttpStatusCode? StatusCode { get; private set; }
    }
}