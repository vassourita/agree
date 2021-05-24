defmodule Accord.Servers.Server do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @required_fields [:name, :privacy]

  schema "server" do
    field :description, :string
    field :name, :string
    field :privacy, :integer

    timestamps()
  end

  @doc false
  def changeset(server, attrs) do
    server
    |> cast(attrs, @required_fields)
    |> validate_required(@required_fields)
    |> validate_number(:privacy, less_than_or_equal_to: 2, greater_than_or_equal_to: 0)
  end
end
