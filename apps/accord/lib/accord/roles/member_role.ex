defmodule Accord.Roles.MemberRole do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  schema "member_role" do
    field :role_id, :binary_id
    field :member_id, :string

    timestamps()
  end

  @doc false
  def changeset(member_role, attrs) do
    member_role
    |> cast(attrs, [])
    |> validate_required([])
  end
end
