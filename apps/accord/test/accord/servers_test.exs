defmodule Accord.ServersTest do
  use Accord.DataCase

  alias Accord.Servers

  describe "server" do
    alias Accord.Servers.Server

    @valid_attrs %{description: "some description", name: "some name", privacy: 42}
    @update_attrs %{description: "some updated description", name: "some updated name", privacy: 43}
    @invalid_attrs %{description: nil, name: nil, privacy: nil}

    def server_fixture(attrs \\ %{}) do
      {:ok, server} =
        attrs
        |> Enum.into(@valid_attrs)
        |> Servers.create_server()

      server
    end

    test "list_server/0 returns all server" do
      server = server_fixture()
      assert Servers.list_server() == [server]
    end

    test "get_server!/1 returns the server with given id" do
      server = server_fixture()
      assert Servers.get_server!(server.id) == server
    end

    test "create_server/1 with valid data creates a server" do
      assert {:ok, %Server{} = server} = Servers.create_server(@valid_attrs)
      assert server.description == "some description"
      assert server.name == "some name"
      assert server.privacy == 42
    end

    test "create_server/1 with invalid data returns error changeset" do
      assert {:error, %Ecto.Changeset{}} = Servers.create_server(@invalid_attrs)
    end

    test "update_server/2 with valid data updates the server" do
      server = server_fixture()
      assert {:ok, %Server{} = server} = Servers.update_server(server, @update_attrs)
      assert server.description == "some updated description"
      assert server.name == "some updated name"
      assert server.privacy == 43
    end

    test "update_server/2 with invalid data returns error changeset" do
      server = server_fixture()
      assert {:error, %Ecto.Changeset{}} = Servers.update_server(server, @invalid_attrs)
      assert server == Servers.get_server!(server.id)
    end

    test "delete_server/1 deletes the server" do
      server = server_fixture()
      assert {:ok, %Server{}} = Servers.delete_server(server)
      assert_raise Ecto.NoResultsError, fn -> Servers.get_server!(server.id) end
    end

    test "change_server/1 returns a server changeset" do
      server = server_fixture()
      assert %Ecto.Changeset{} = Servers.change_server(server)
    end
  end

  describe "category" do
    alias Accord.Servers.Category

    @valid_attrs %{name: "some name"}
    @update_attrs %{name: "some updated name"}
    @invalid_attrs %{name: nil}

    def category_fixture(attrs \\ %{}) do
      {:ok, category} =
        attrs
        |> Enum.into(@valid_attrs)
        |> Servers.create_category()

      category
    end

    test "list_category/0 returns all category" do
      category = category_fixture()
      assert Servers.list_category() == [category]
    end

    test "get_category!/1 returns the category with given id" do
      category = category_fixture()
      assert Servers.get_category!(category.id) == category
    end

    test "create_category/1 with valid data creates a category" do
      assert {:ok, %Category{} = category} = Servers.create_category(@valid_attrs)
      assert category.name == "some name"
    end

    test "create_category/1 with invalid data returns error changeset" do
      assert {:error, %Ecto.Changeset{}} = Servers.create_category(@invalid_attrs)
    end

    test "update_category/2 with valid data updates the category" do
      category = category_fixture()
      assert {:ok, %Category{} = category} = Servers.update_category(category, @update_attrs)
      assert category.name == "some updated name"
    end

    test "update_category/2 with invalid data returns error changeset" do
      category = category_fixture()
      assert {:error, %Ecto.Changeset{}} = Servers.update_category(category, @invalid_attrs)
      assert category == Servers.get_category!(category.id)
    end

    test "delete_category/1 deletes the category" do
      category = category_fixture()
      assert {:ok, %Category{}} = Servers.delete_category(category)
      assert_raise Ecto.NoResultsError, fn -> Servers.get_category!(category.id) end
    end

    test "change_category/1 returns a category changeset" do
      category = category_fixture()
      assert %Ecto.Changeset{} = Servers.change_category(category)
    end
  end

  describe "channel" do
    alias Accord.Servers.Channel

    @valid_attrs %{name: "some name"}
    @update_attrs %{name: "some updated name"}
    @invalid_attrs %{name: nil}

    def channel_fixture(attrs \\ %{}) do
      {:ok, channel} =
        attrs
        |> Enum.into(@valid_attrs)
        |> Servers.create_channel()

      channel
    end

    test "list_channel/0 returns all channel" do
      channel = channel_fixture()
      assert Servers.list_channel() == [channel]
    end

    test "get_channel!/1 returns the channel with given id" do
      channel = channel_fixture()
      assert Servers.get_channel!(channel.id) == channel
    end

    test "create_channel/1 with valid data creates a channel" do
      assert {:ok, %Channel{} = channel} = Servers.create_channel(@valid_attrs)
      assert channel.name == "some name"
    end

    test "create_channel/1 with invalid data returns error changeset" do
      assert {:error, %Ecto.Changeset{}} = Servers.create_channel(@invalid_attrs)
    end

    test "update_channel/2 with valid data updates the channel" do
      channel = channel_fixture()
      assert {:ok, %Channel{} = channel} = Servers.update_channel(channel, @update_attrs)
      assert channel.name == "some updated name"
    end

    test "update_channel/2 with invalid data returns error changeset" do
      channel = channel_fixture()
      assert {:error, %Ecto.Changeset{}} = Servers.update_channel(channel, @invalid_attrs)
      assert channel == Servers.get_channel!(channel.id)
    end

    test "delete_channel/1 deletes the channel" do
      channel = channel_fixture()
      assert {:ok, %Channel{}} = Servers.delete_channel(channel)
      assert_raise Ecto.NoResultsError, fn -> Servers.get_channel!(channel.id) end
    end

    test "change_channel/1 returns a channel changeset" do
      channel = channel_fixture()
      assert %Ecto.Changeset{} = Servers.change_channel(channel)
    end
  end

  describe "member" do
    alias Accord.Servers.Member

    @valid_attrs %{id: "some id"}
    @update_attrs %{id: "some updated id"}
    @invalid_attrs %{id: nil}

    def member_fixture(attrs \\ %{}) do
      {:ok, member} =
        attrs
        |> Enum.into(@valid_attrs)
        |> Servers.create_member()

      member
    end

    test "list_member/0 returns all member" do
      member = member_fixture()
      assert Servers.list_member() == [member]
    end

    test "get_member!/1 returns the member with given id" do
      member = member_fixture()
      assert Servers.get_member!(member.id) == member
    end

    test "create_member/1 with valid data creates a member" do
      assert {:ok, %Member{} = member} = Servers.create_member(@valid_attrs)
      assert member.id == "some id"
    end

    test "create_member/1 with invalid data returns error changeset" do
      assert {:error, %Ecto.Changeset{}} = Servers.create_member(@invalid_attrs)
    end

    test "update_member/2 with valid data updates the member" do
      member = member_fixture()
      assert {:ok, %Member{} = member} = Servers.update_member(member, @update_attrs)
      assert member.id == "some updated id"
    end

    test "update_member/2 with invalid data returns error changeset" do
      member = member_fixture()
      assert {:error, %Ecto.Changeset{}} = Servers.update_member(member, @invalid_attrs)
      assert member == Servers.get_member!(member.id)
    end

    test "delete_member/1 deletes the member" do
      member = member_fixture()
      assert {:ok, %Member{}} = Servers.delete_member(member)
      assert_raise Ecto.NoResultsError, fn -> Servers.get_member!(member.id) end
    end

    test "change_member/1 returns a member changeset" do
      member = member_fixture()
      assert %Ecto.Changeset{} = Servers.change_member(member)
    end
  end
end
