namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

public class ClientApplicationAccessKeyEqualSpecification : Specification<ClientApplication>
{
    public ClientApplicationAccessKeyEqualSpecification(string accessKey)
        => Expression = u
        => u.AccessKey == accessKey;
}