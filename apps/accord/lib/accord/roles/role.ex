defmodule Accord.Roles.Role do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @required_fields [
    :name,
    :can_update_server_name,
    :can_update_server_description,
    :can_update_server_privacy,
    :can_add_users,
    :can_remove_users,
    :server_id
  ]

  schema "role" do
    field :can_add_users, :boolean, default: false
    field :can_remove_users, :boolean, default: false
    field :can_update_server_description, :boolean, default: false
    field :can_update_server_name, :boolean, default: false
    field :can_update_server_privacy, :boolean, default: false
    field :name, :string
    field :server_id, :binary_id

    timestamps()
  end

  @doc false
  def changeset(role, attrs) do
    role
    |> cast(attrs, @required_fields)
    |> validate_required(@required_fields)
  end
end
