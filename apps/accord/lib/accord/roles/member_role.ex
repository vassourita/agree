defmodule Accord.Roles.MemberRole do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key false
  @foreign_key_type :binary_id
  @required_fields [:role_id, :member_id]

  schema "member_role" do
    field :role_id, :binary_id, primary_key: true
    field :member_id, :binary_id, primary_key: true

    timestamps()
  end

  @doc false
  def changeset(member_role, attrs) do
    member_role
    |> cast(attrs, @required_fields)
    |> validate_required(@required_fields)
  end
end
