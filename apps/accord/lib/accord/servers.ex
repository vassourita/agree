defmodule Accord.Servers do
  @moduledoc """
  The Servers context.
  """

  import Ecto.Query, warn: false
  alias Ecto.Multi
  alias Accord.Repo

  alias Accord.Servers.{Server, Category, Channel, Member}
  alias Accord.Roles

  def list_servers do
    Server
    |> Repo.all()
  end

  def get_server!(server_id, user) do
    is_member =
      Member
      |> where(allow_user_id: ^user.id, server_id: ^server_id)
      |> Repo.one()
      |> is_nil()
      |> Kernel.not()

    if is_member do
      s =
        from(s in Server, where: s.id == ^server_id)
        |> preload(categories: [:channels])
        |> preload(:roles)
        |> preload(members: [:roles])
        |> Repo.one()

      if is_nil(s) do
        {:error, %{reason: :not_found, resource_name: "Server"}}
      else
        {:ok, s}
      end
    else
      s =
        from(s in Server, where: s.id == ^server_id and s.privacy < 2)
        |> Repo.one()

      if is_nil(s) do
        {:error, %{reason: :not_found, resource_name: "Server"}}
      else
        {:ok, s}
      end
    end
  end

  def create_server(server_attrs, user) do
    Repo.transaction(fn ->
      {:ok, server} =
        %Server{}
        |> Server.changeset(server_attrs)
        |> Repo.insert()

      {:ok, category} =
        create_category(%{server_id: server.id, name: "Welcome to #{server.name}"})

      {:ok, _channel} = create_channel(%{category_id: category.id, name: "Example channel"})

      {:ok, member} = create_member(%{server_id: server.id, allow_user_id: user.id})

      {:ok, admin_role} = Roles.create_default_admin_role(server.id)

      member
      |> Member.changeset_add_role(admin_role)

      Server
      |> where(id: ^server.id)
      |> preload(categories: [:channels])
      |> preload(:roles)
      |> preload(members: [:roles])
      |> Repo.one()
    end)
  end

  @doc """
  Updates a server.

  ## Examples

      iex> update_server(server, %{field: new_value})
      {:ok, %Server{}}

      iex> update_server(server, %{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def update_server(%Server{} = server, attrs) do
    server
    |> Server.changeset(attrs)
    |> Repo.update()
  end

  @doc """
  Deletes a server.

  ## Examples

      iex> delete_server(server)
      {:ok, %Server{}}

      iex> delete_server(server)
      {:error, %Ecto.Changeset{}}

  """
  def delete_server(%Server{} = server) do
    Repo.delete(server)
  end

  @doc """
  Returns an `%Ecto.Changeset{}` for tracking server changes.

  ## Examples

      iex> change_server(server)
      %Ecto.Changeset{data: %Server{}}

  """
  def change_server(%Server{} = server, attrs \\ %{}) do
    Server.changeset(server, attrs)
  end

  def int_to_privacy_string(number) do
    case number do
      0 -> "public"
      1 -> "private"
      2 -> "secret"
      _ -> nil
    end
  end

  def privacy_string_to_int(str) do
    case str do
      "public" -> 0
      "private" -> 1
      "secret" -> 2
      _ -> nil
    end
  end

  @doc """
  Returns the list of category.

  ## Examples

      iex> list_category()
      [%Category{}, ...]

  """
  def list_category(server_id) do
    Repo.all(from c in Category, where: c.server_id == ^server_id)
  end

  @doc """
  Creates a category.

  ## Examples

      iex> create_category(%{field: value})
      {:ok, %Category{}}

      iex> create_category(%{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def create_category(attrs \\ %{}) do
    %Category{}
    |> Category.changeset(attrs)
    |> Repo.insert()
  end

  @doc """
  Updates a category.

  ## Examples

      iex> update_category(category, %{field: new_value})
      {:ok, %Category{}}

      iex> update_category(category, %{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def update_category(%Category{} = category, attrs) do
    category
    |> Category.changeset(attrs)
    |> Repo.update()
  end

  @doc """
  Deletes a category.

  ## Examples

      iex> delete_category(category)
      {:ok, %Category{}}

      iex> delete_category(category)
      {:error, %Ecto.Changeset{}}

  """
  def delete_category(%Category{} = category) do
    Repo.delete(category)
  end

  @doc """
  Returns an `%Ecto.Changeset{}` for tracking category changes.

  ## Examples

      iex> change_category(category)
      %Ecto.Changeset{data: %Category{}}

  """
  def change_category(%Category{} = category, attrs \\ %{}) do
    Category.changeset(category, attrs)
  end

  @doc """
  Returns the list of channel.

  ## Examples

      iex> list_channel()
      [%Channel{}, ...]

  """
  def list_channel(category_id) do
    Repo.all(from c in Channel, where: c.category_id == ^category_id)
  end

  @doc """
  Creates a channel.

  ## Examples

      iex> create_channel(%{field: value})
      {:ok, %Channel{}}

      iex> create_channel(%{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def create_channel(attrs \\ %{}) do
    %Channel{}
    |> Channel.changeset(attrs)
    |> Repo.insert()
  end

  @doc """
  Updates a channel.

  ## Examples

      iex> update_channel(channel, %{field: new_value})
      {:ok, %Channel{}}

      iex> update_channel(channel, %{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def update_channel(%Channel{} = channel, attrs) do
    channel
    |> Channel.changeset(attrs)
    |> Repo.update()
  end

  @doc """
  Deletes a channel.

  ## Examples

      iex> delete_channel(channel)
      {:ok, %Channel{}}

      iex> delete_channel(channel)
      {:error, %Ecto.Changeset{}}

  """
  def delete_channel(%Channel{} = channel) do
    Repo.delete(channel)
  end

  @doc """
  Returns an `%Ecto.Changeset{}` for tracking channel changes.

  ## Examples

      iex> change_channel(channel)
      %Ecto.Changeset{data: %Channel{}}

  """
  def change_channel(%Channel{} = channel, attrs \\ %{}) do
    Channel.changeset(channel, attrs)
  end

  @doc """
  Returns the list of member.

  ## Examples

      iex> list_member()
      [%Member{}, ...]

  """
  def list_member do
    Repo.all(Member)
  end

  @doc """
  Gets a single member.

  Raises `Ecto.NoResultsError` if the Member does not exist.

  ## Examples

      iex> get_member!(123)
      %Member{}

      iex> get_member!(456)
      ** (Ecto.NoResultsError)

  """
  def get_member!(id), do: Repo.get!(Member, id)

  @doc """
  Creates a member.

  ## Examples

      iex> create_member(%{field: value})
      {:ok, %Member{}}

      iex> create_member(%{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def create_member(attrs \\ %{}) do
    %Member{}
    |> Member.changeset(attrs)
    |> Repo.insert()
  end

  @doc """
  Updates a member.

  ## Examples

      iex> update_member(member, %{field: new_value})
      {:ok, %Member{}}

      iex> update_member(member, %{field: bad_value})
      {:error, %Ecto.Changeset{}}

  """
  def update_member(%Member{} = member, attrs) do
    member
    |> Member.changeset(attrs)
    |> Repo.update()
  end

  @doc """
  Deletes a member.

  ## Examples

      iex> delete_member(member)
      {:ok, %Member{}}

      iex> delete_member(member)
      {:error, %Ecto.Changeset{}}

  """
  def delete_member(%Member{} = member) do
    Repo.delete(member)
  end

  @doc """
  Returns an `%Ecto.Changeset{}` for tracking member changes.

  ## Examples

      iex> change_member(member)
      %Ecto.Changeset{data: %Member{}}

  """
  def change_member(%Member{} = member, attrs \\ %{}) do
    Member.changeset(member, attrs)
  end
end
