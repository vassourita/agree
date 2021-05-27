defmodule Accord.Servers.Member do
  use Ecto.Schema
  import Ecto.Changeset

  alias Accord.Repo

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @required_fields [:allow_user_id, :server_id]

  schema "member" do
    field :allow_user_id, :string
    belongs_to :server, Accord.Servers.Server
    many_to_many :roles, Accord.Roles.Role, join_through: Accord.Roles.MemberRole

    timestamps()
  end

  @doc false
  def changeset(member, attrs) do
    member
    |> cast(attrs, @required_fields)
    |> validate_required(@required_fields)
  end

  @doc false
  def changeset_add_role(member, role) do
    member
    |> Repo.preload(:roles)
    |> cast(%{}, @required_fields)
    |> put_assoc(:roles, [role])
  end
end
