defmodule Accord.AccountTest do
  use Accord.DataCase

  alias Accord.Account

  describe "users" do
    alias Accord.Account.User

    @valid_attrs %{
      email: "some@email.com",
      email_verified: false,
      tag: 9999,
      user_name: "some user_name",
      password: "some password"
    }
    @update_attrs %{
      email: "some@updated.email.com",
      email_verified: true,
      tag: 1,
      user_name: "some updated user_name",
      password: "some updated password"
    }
    @invalid_attrs %{
      avatar_url: nil,
      email: nil,
      email_verified: nil,
      password_hash: nil,
      tag: nil,
      user_name: nil,
      password: nil
    }

    def user_fixture(attrs \\ %{}) do
      {:ok, user} =
        attrs
        |> Enum.into(@valid_attrs)
        |> Account.create_user()

      user
    end

    def user_without_password(attrs \\ %{}) do
      %{user_fixture(attrs) | password: nil}
    end

    test "list_users/0 returns all users" do
      user = user_without_password()
      assert Account.list_users() == [user]
    end

    test "get_user!/1 returns the user with given id" do
      user = user_without_password()
      assert Account.get_user!(user.id) == user
    end

    test "create_user/1 with valid data creates a user" do
      assert {:ok, %User{} = user} = Account.create_user(@valid_attrs)
      assert user.email == "some@email.com"
      assert user.email_verified == false
      assert user.tag <= 9999
      assert user.tag >= 1
      assert user.user_name == "some user_name"
      assert Bcrypt.verify_pass("some password", user.password_hash)
    end

    test "create_user/1 with invalid data returns error changeset" do
      assert {:error, %Ecto.Changeset{}} = Account.create_user(@invalid_attrs)
    end

    test "update_user/2 with valid data updates the user" do
      user = user_fixture()
      assert {:ok, %User{} = user} = Account.update_user(user, @update_attrs)
      assert user.email == "some@updated.email.com"
      assert user.email_verified == false
      assert user.tag <= 9999
      assert user.tag >= 1
      assert user.tag == 1
      assert user.user_name == "some updated user_name"
      assert Bcrypt.verify_pass("some updated password", user.password_hash)
    end

    test "update_user/2 with invalid data returns error changeset" do
      user = user_without_password()
      assert {:error, %Ecto.Changeset{}} = Account.update_user(user, @invalid_attrs)
      assert user == Account.get_user!(user.id)
    end

    test "update_user/2 with tag already in use return error changeset" do
      user = user_without_password()
      assert {:error, %Ecto.Changeset{}} = Account.update_user(user, %{tag: user.tag})
      assert user == Account.get_user!(user.id)
    end

    test "delete_user/1 deletes the user" do
      user = user_fixture()
      assert {:ok, %User{}} = Account.delete_user(user)
      assert_raise Ecto.NoResultsError, fn -> Account.get_user!(user.id) end
    end

    test "change_user/1 returns a user changeset" do
      user = user_fixture()
      assert %Ecto.Changeset{} = Account.change_user(user)
    end
  end
end
