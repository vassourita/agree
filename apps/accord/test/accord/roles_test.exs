defmodule Accord.RolesTest do
  use Accord.DataCase

  alias Accord.Roles

  describe "role" do
    alias Accord.Roles.Role

    @valid_attrs %{can_add_users: true, can_remove_users: true, can_update_server_description: true, can_update_server_name: true, can_update_server_privacy: true, name: "some name"}
    @update_attrs %{can_add_users: false, can_remove_users: false, can_update_server_description: false, can_update_server_name: false, can_update_server_privacy: false, name: "some updated name"}
    @invalid_attrs %{can_add_users: nil, can_remove_users: nil, can_update_server_description: nil, can_update_server_name: nil, can_update_server_privacy: nil, name: nil}

    def role_fixture(attrs \\ %{}) do
      {:ok, role} =
        attrs
        |> Enum.into(@valid_attrs)
        |> Roles.create_role()

      role
    end

    test "list_role/0 returns all role" do
      role = role_fixture()
      assert Roles.list_role() == [role]
    end

    test "get_role!/1 returns the role with given id" do
      role = role_fixture()
      assert Roles.get_role!(role.id) == role
    end

    test "create_role/1 with valid data creates a role" do
      assert {:ok, %Role{} = role} = Roles.create_role(@valid_attrs)
      assert role.can_add_users == true
      assert role.can_remove_users == true
      assert role.can_update_server_description == true
      assert role.can_update_server_name == true
      assert role.can_update_server_privacy == true
      assert role.name == "some name"
    end

    test "create_role/1 with invalid data returns error changeset" do
      assert {:error, %Ecto.Changeset{}} = Roles.create_role(@invalid_attrs)
    end

    test "update_role/2 with valid data updates the role" do
      role = role_fixture()
      assert {:ok, %Role{} = role} = Roles.update_role(role, @update_attrs)
      assert role.can_add_users == false
      assert role.can_remove_users == false
      assert role.can_update_server_description == false
      assert role.can_update_server_name == false
      assert role.can_update_server_privacy == false
      assert role.name == "some updated name"
    end

    test "update_role/2 with invalid data returns error changeset" do
      role = role_fixture()
      assert {:error, %Ecto.Changeset{}} = Roles.update_role(role, @invalid_attrs)
      assert role == Roles.get_role!(role.id)
    end

    test "delete_role/1 deletes the role" do
      role = role_fixture()
      assert {:ok, %Role{}} = Roles.delete_role(role)
      assert_raise Ecto.NoResultsError, fn -> Roles.get_role!(role.id) end
    end

    test "change_role/1 returns a role changeset" do
      role = role_fixture()
      assert %Ecto.Changeset{} = Roles.change_role(role)
    end
  end

  describe "member_role" do
    alias Accord.Roles.MemberRole

    @valid_attrs %{}
    @update_attrs %{}
    @invalid_attrs %{}

    def member_role_fixture(attrs \\ %{}) do
      {:ok, member_role} =
        attrs
        |> Enum.into(@valid_attrs)
        |> Roles.create_member_role()

      member_role
    end

    test "list_member_role/0 returns all member_role" do
      member_role = member_role_fixture()
      assert Roles.list_member_role() == [member_role]
    end

    test "get_member_role!/1 returns the member_role with given id" do
      member_role = member_role_fixture()
      assert Roles.get_member_role!(member_role.id) == member_role
    end

    test "create_member_role/1 with valid data creates a member_role" do
      assert {:ok, %MemberRole{} = member_role} = Roles.create_member_role(@valid_attrs)
    end

    test "create_member_role/1 with invalid data returns error changeset" do
      assert {:error, %Ecto.Changeset{}} = Roles.create_member_role(@invalid_attrs)
    end

    test "update_member_role/2 with valid data updates the member_role" do
      member_role = member_role_fixture()
      assert {:ok, %MemberRole{} = member_role} = Roles.update_member_role(member_role, @update_attrs)
    end

    test "update_member_role/2 with invalid data returns error changeset" do
      member_role = member_role_fixture()
      assert {:error, %Ecto.Changeset{}} = Roles.update_member_role(member_role, @invalid_attrs)
      assert member_role == Roles.get_member_role!(member_role.id)
    end

    test "delete_member_role/1 deletes the member_role" do
      member_role = member_role_fixture()
      assert {:ok, %MemberRole{}} = Roles.delete_member_role(member_role)
      assert_raise Ecto.NoResultsError, fn -> Roles.get_member_role!(member_role.id) end
    end

    test "change_member_role/1 returns a member_role changeset" do
      member_role = member_role_fixture()
      assert %Ecto.Changeset{} = Roles.change_member_role(member_role)
    end
  end
end
