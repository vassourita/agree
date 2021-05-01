defmodule Accord.User do
  use Ecto.Schema
  import Ecto.Changeset
  import Ecto.Query
  alias Ecto.Changeset

  alias Accord.Repo

  @primary_key {:id, :binary_id, autogenerate: true}

  @required_params [:user_name, :email, :password]

  schema "users" do
    field :email, :string
    field :email_verified, :boolean
    field :user_name, :string
    field :tag, :integer
    field :password, :string, virtual: true
    field :password_hash, :string
    field :avatar_url, :string

    timestamps()
  end

  def changeset(params) do
    %__MODULE__{}
    |> cast(params, @required_params)
    |> validate_required(@required_params)
    |> validate_length(:password, min: 5)
    |> validate_length(:user_name, min: 1, max: 40)
    |> validate_format(:email, ~r/@/)
    |> unique_constraint([:email])
    |> put_tag()
    |> put_email_verified()
    |> put_password_hash()
  end

  defp put_tag(%Changeset{valid?: true, changes: %{user_name: user_name}} = changeset) do
    tags_in_use = Repo.all(from(u in __MODULE__, select: u.tag, where: u.user_name == ^user_name))

    change(changeset, %{tag: gen_tag(user_name, tags_in_use)})
  end

  defp put_tag(changeset), do: changeset

  defp gen_tag(user_name, tags) do
    new_tag = Enum.random(1..9999)

    if new_tag in tags do
      gen_tag(user_name, tags)
    else
      new_tag
    end
  end

  defp put_email_verified(%Changeset{valid?: true} = changeset) do
    change(changeset, %{email_verified: false})
  end

  defp put_email_verified(changeset), do: changeset

  defp put_password_hash(%Changeset{valid?: true, changes: %{password: password}} = changeset) do
    change(changeset, Bcrypt.add_hash(password))
  end

  defp put_password_hash(changeset), do: changeset
end
