using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.ValueObjects;

namespace LibraryManagement.Domain.Entities;

public sealed class Member : AggregateRoot
{
    public FullName FullName { get; private set; } = null!;

    public Email Email { get; private set; } = null!;

    public MemberStatus Status { get; private set; }

    private Member(
        Guid id,
        FullName fullName,
        Email email)
        : base(id)
    {
        FullName = fullName;
        Email = email;
        Status = MemberStatus.Active;
    }

    private Member()
    {
    }

    public static Member Create(
        FullName fullName,
        Email email)
    {
        return new Member(
            Guid.NewGuid(),
            fullName,
            email);
    }

    public void ChangeName(FullName fullName)
    {
        FullName = fullName;
    }

    public void ChangeEmail(Email email)
    {
        Email = email;
    }

    public void Activate()
    {
        if (Status == MemberStatus.Active)
            throw new DomainException("Member is already active.");

        Status = MemberStatus.Active;
    }

    public void Deactivate()
    {
        if (Status == MemberStatus.Inactive)
            throw new DomainException("Member is already inactive.");

        Status = MemberStatus.Inactive;
    }

    public void Suspend()
    {
        if (Status == MemberStatus.Suspended)
            throw new DomainException("Member is already suspended.");

        Status = MemberStatus.Suspended;
    }
}