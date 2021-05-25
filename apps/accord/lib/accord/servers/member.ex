defmodule Accord.Servers.Member do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :string, autogenerate: false}
  @foreign_key_type :binary_id
  @required_fields [:id, :server_id]

  schema "member" do
    field :server_id, :binary_id

    timestamps()
  end

  @doc false
  def changeset(member, attrs) do
    member
    |> cast(attrs, @required_fields)
    |> validate_required(@required_fields)
  end
end
