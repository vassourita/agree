defmodule Accord.AccountTest do
  use Accord.DataCase

  alias Accord.Account

  describe "users" do
    alias Accord.Account.User

    @valid_attrs %{avatar_url: "some avatar_url", email: "some email", email_verified: true, password_hash: "some password_hash", tag: 42, user_name: "some user_name"}
    @update_attrs %{avatar_url: "some updated avatar_url", email: "some updated email", email_verified: false, password_hash: "some updated password_hash", tag: 43, user_name: "some updated user_name"}
    @invalid_attrs %{avatar_url: nil, email: nil, email_verified: nil, password_hash: nil, tag: nil, user_name: nil}

    def user_fixture(attrs \\ %{}) do
      {:ok, user} =
        attrs
        |> Enum.into(@valid_attrs)
        |> Account.create_user()

      user
    end

    test "list_users/0 returns all users" do
      user = user_fixture()
      assert Account.list_users() == [user]
    end

    test "get_user!/1 returns the user with given id" do
      user = user_fixture()
      assert Account.get_user!(user.id) == user
    end

    test "create_user/1 with valid data creates a user" do
      assert {:ok, %User{} = user} = Account.create_user(@valid_attrs)
      assert user.avatar_url == "some avatar_url"
      assert user.email == "some email"
      assert user.email_verified == true
      assert user.password_hash == "some password_hash"
      assert user.tag == 42
      assert user.user_name == "some user_name"
    end

    test "create_user/1 with invalid data returns error changeset" do
      assert {:error, %Ecto.Changeset{}} = Account.create_user(@invalid_attrs)
    end

    test "update_user/2 with valid data updates the user" do
      user = user_fixture()
      assert {:ok, %User{} = user} = Account.update_user(user, @update_attrs)
      assert user.avatar_url == "some updated avatar_url"
      assert user.email == "some updated email"
      assert user.email_verified == false
      assert user.password_hash == "some updated password_hash"
      assert user.tag == 43
      assert user.user_name == "some updated user_name"
    end

    test "update_user/2 with invalid data returns error changeset" do
      user = user_fixture()
      assert {:error, %Ecto.Changeset{}} = Account.update_user(user, @invalid_attrs)
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
