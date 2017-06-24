using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Domain.Dto
{
    public class BillingAccount
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        public long ClientId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public Guid? PackageId { get; set; }
        public string Active { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class BillingAccountValidator : AbstractValidator<BillingAccount>
    {
        public BillingAccountValidator()
        {
            RuleFor(x => x.ItemCode).NotEmpty().NotNull();
            RuleFor(x => x.ItemDescription).NotEmpty().NotNull();
            RuleFor(x => x.Active).NotEmpty().NotNull();
            RuleFor(x => x.StartDate).NotNull();
        }
    }
}
