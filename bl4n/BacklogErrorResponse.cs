// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogErrorResponse.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Runtime.Serialization;

namespace BL4N
{
    /// <summary> �G���[������\���܂� </summary>
    [DataContract]
    public class BacklogErrorResponse
    {
        /// <summary> HTTP �̃X�e�[�^�X�R�[�h���擾�܂��͐ݒ肵�܂� </summary>
        public HttpStatusCode Statuscode { get; set; }

        /// <summary> �G���[���̈ꗗ���擾���܂� </summary>
        [DataMember(Name = "errors")]
        public BacklogErrorInfo[] Errors { get; private set; }
    }
}