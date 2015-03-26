// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotification.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace BL4N.Data
{
    /// <summary> notification </summary>
    public interface INotification
    {
        long Id { get; }

        bool AlreadyRead { get; }

        int Reason { get; }

        IUser User { get; }

        bool ResouceAlreadyRead { get; }
    }
}