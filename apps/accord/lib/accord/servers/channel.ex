defmodule Accord.Servers.Channel do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @required_fields [:name, :category_id]

  schema "channel" do
    field :name, :string
    field :category_id, :binary_id

    timestamps()
  end

  @doc false
  def changeset(channel, attrs) do
    channel
    |> cast(attrs, @required_fields)
    |> validate_required(@required_fields)
  end
end
