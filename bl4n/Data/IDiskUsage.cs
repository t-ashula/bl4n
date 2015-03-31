using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N
{
    public interface IDiskUsage
    {
        long Capacity { get; }

        long Issue { get; }

        long Wiki { get; }

        long File { get; }

        long Subversion { get; }

        long Git { get; }

        IList<IDiskUsageDetail> Details { get; }
    }

    public interface IDiskUsageDetail
    {
        long ProjectId { get; }

        long Issue { get; }

        long Wiki { get; }

        long File { get; }

        long Subversion { get; }

        long Git { get; }
    }

    [DataContract]
    internal sealed class DiskUsage : IDiskUsage
    {
        [DataMember(Name = "capacity")]
        public long Capacity { get; private set; }

        [DataMember(Name = "issue")]
        public long Issue { get; private set; }

        [DataMember(Name = "wiki")]
        public long Wiki { get; private set; }

        [DataMember(Name = "file")]
        public long File { get; private set; }

        [DataMember(Name = "subversion")]
        public long Subversion { get; private set; }

        [DataMember(Name = "git")]
        public long Git { get; private set; }

        [DataMember(Name = "details")]
        private List<DiskUsageDetail> _details;

        public IList<IDiskUsageDetail> Details
        {
            get { return _details.ToList<IDiskUsageDetail>(); }
        }
    }

    [DataContract]
    internal sealed class DiskUsageDetail : IDiskUsageDetail
    {
        [DataMember(Name = "projectId")]
        public long ProjectId { get; private set; }

        [DataMember(Name = "issue")]
        public long Issue { get; private set; }

        [DataMember(Name = "wiki")]
        public long Wiki { get; private set; }

        [DataMember(Name = "file")]
        public long File { get; private set; }

        [DataMember(Name = "subversion")]
        public long Subversion { get; private set; }

        [DataMember(Name = "git")]
        public long Git { get; private set; }
    }
}