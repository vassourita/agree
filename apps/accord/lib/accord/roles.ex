defmodule Accord.Roles do
  @moduledoc """
  The Roles context.
  """

  import Ecto.Query, warn: false
  alias Accord.Repo

  alias Accord.Roles.Role
  alias Accord.Roles.MemberRole
  alias Accord.Servers.Server

  @doc """
  Returns the list of role.

  ## Examples

      iex> list_role()
      [%Role{}, ...]

  """
  def list_role do
    Repo.all(Role)
  end

  @doc """
  Gets a single role.

  Raises `Ecto.NoResultsError` if the Role does not exist.

  ## Examples

      iex> get_role!(123)
      %Role{}

      iex> get_role!(456)
      ** (Ecto.NoResultsError)

  """
  def get_role!(id), do: Repo.get!(Role, id)

  @doc """
  Creates a role.

  ## Examples

      iex> create_role(%{field: value})
      {:ok, %Role{}}

      iex> create_role(%{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def create_role(attrs \\ %{}) do
    %Role{}
    |> Role.changeset(attrs)
    |> Repo.insert()
  end

  def create_default_admin_role(server_id) do
    %Role{}
    |> Role.changeset(%{
      name: "Admin",
      server_id: server_id,
      can_add_users: true,
      can_remove_users: true,
      can_update_server_description: true,
      can_update_server_name: true,
      can_update_server_privacy: true
    })
    |> Repo.insert()
  end

  def get_member_permissions_on_server(user_id, server_id) do
    query =
      from mr in MemberRole,
        join: r in Role,
        on: r.id == mr.role_id,
        join: s in Server,
        on: s.id == r.server_id,
        where: mr.member_id == ^user_id and s.id == ^server_id

    {:ok, member_roles} = Repo.one!(query)
  end

  @doc """
  Updates a role.

  ## Examples

      iex> update_role(role, %{field: new_value})
      {:ok, %Role{}}

      iex> update_role(role, %{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def update_role(%Role{} = role, attrs) do
    role
    |> Role.changeset(attrs)
    |> Repo.update()
  end

  @doc """
  Deletes a role.

  ## Examples

      iex> delete_role(role)
      {:ok, %Role{}}

      iex> delete_role(role)
      {:error, %Ecto.Changeset{}}

  """
  def delete_role(%Role{} = role) do
    Repo.delete(role)
  end

  @doc """
  Returns an `%Ecto.Changeset{}` for tracking role changes.

  ## Examples

      iex> change_role(role)
      %Ecto.Changeset{data: %Role{}}

  """
  def change_role(%Role{} = role, attrs \\ %{}) do
    Role.changeset(role, attrs)
  end

  @doc """
  Returns the list of member_role.

  ## Examples

      iex> list_member_role()
      [%MemberRole{}, ...]

  """
  def list_member_role do
    Repo.all(MemberRole)
  end

  @doc """
  Gets a single member_role.

  Raises `Ecto.NoResultsError` if the Member role does not exist.

  ## Examples

      iex> get_member_role!(123)
      %MemberRole{}

      iex> get_member_role!(456)
      ** (Ecto.NoResultsError)

  """
  def get_member_role!(id), do: Repo.get!(MemberRole, id)

  @doc """
  Creates a member_role.

  ## Examples

      iex> create_member_role(%{field: value})
      {:ok, %MemberRole{}}

      iex> create_member_role(%{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def create_member_role(attrs \\ %{}) do
    %MemberRole{}
    |> MemberRole.changeset(attrs)
    |> Repo.insert()
  end

  @doc """
  Updates a member_role.

  ## Examples

      iex> update_member_role(member_role, %{field: new_value})
      {:ok, %MemberRole{}}

      iex> update_member_role(member_role, %{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def update_member_role(%MemberRole{} = member_role, attrs) do
    member_role
    |> MemberRole.changeset(attrs)
    |> Repo.update()
  end

  @doc """
  Deletes a member_role.

  ## Examples

      iex> delete_member_role(member_role)
      {:ok, %MemberRole{}}

      iex> delete_member_role(member_role)
      {:error, %Ecto.Changeset{}}

  """
  def delete_member_role(%MemberRole{} = member_role) do
    Repo.delete(member_role)
  end

  @doc """
  Returns an `%Ecto.Changeset{}` for tracking member_role changes.

  ## Examples

      iex> change_member_role(member_role)
      %Ecto.Changeset{data: %MemberRole{}}

  """
  def change_member_role(%MemberRole{} = member_role, attrs \\ %{}) do
    MemberRole.changeset(member_role, attrs)
  end
end
