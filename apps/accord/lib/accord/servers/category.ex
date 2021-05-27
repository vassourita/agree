defmodule Accord.Servers.Category do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @required_fields [:name, :server_id]

  schema "category" do
    field :name, :string
    belongs_to :server, Accord.Servers.Server
    has_many :channels, Accord.Servers.Channel

    timestamps()
  end

  @doc false
  def changeset(category, attrs) do
    category
    |> cast(attrs, @required_fields)
    |> validate_required(@required_fields)
    |> validate_length(:name, min: 1, max: 50)
  end
end
