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
    has_many :categories, Accord.Servers.Category
    has_many :members, Accord.Servers.Member
    has_many :roles, Accord.Roles.Role

    timestamps()
  end

  @doc false
  def changeset(server, attrs) do
    server
    |> cast(attrs, @required_fields)
    |> validate_required(@required_fields)
    |> validate_inclusion(:privacy, 0..2)
  end
end
