defmodule AccordWeb.RoleControllerTest do
  use AccordWeb.ConnCase

  alias Accord.Roles
  alias Accord.Roles.Role

  @create_attrs %{
    can_add_users: true,
    can_remove_users: true,
    can_update_server_description: true,
    can_update_server_name: true,
    can_update_server_privacy: true,
    name: "some name"
  }
  @update_attrs %{
    can_add_users: false,
    can_remove_users: false,
    can_update_server_description: false,
    can_update_server_name: false,
    can_update_server_privacy: false,
    name: "some updated name"
  }
  @invalid_attrs %{can_add_users: nil, can_remove_users: nil, can_update_server_description: nil, can_update_server_name: nil, can_update_server_privacy: nil, name: nil}

  def fixture(:role) do
    {:ok, role} = Roles.create_role(@create_attrs)
    role
  end

  setup %{conn: conn} do
    {:ok, conn: put_req_header(conn, "accept", "application/json")}
  end

  describe "index" do
    test "lists all role", %{conn: conn} do
      conn = get(conn, Routes.role_path(conn, :index))
      assert json_response(conn, 200)["data"] == []
    end
  end

  describe "create role" do
    test "renders role when data is valid", %{conn: conn} do
      conn = post(conn, Routes.role_path(conn, :create), role: @create_attrs)
      assert %{"id" => id} = json_response(conn, 201)["data"]

      conn = get(conn, Routes.role_path(conn, :show, id))

      assert %{
               "id" => id,
               "can_add_users" => true,
               "can_remove_users" => true,
               "can_update_server_description" => true,
               "can_update_server_name" => true,
               "can_update_server_privacy" => true,
               "name" => "some name"
             } = json_response(conn, 200)["data"]
    end

    test "renders errors when data is invalid", %{conn: conn} do
      conn = post(conn, Routes.role_path(conn, :create), role: @invalid_attrs)
      assert json_response(conn, 422)["errors"] != %{}
    end
  end

  describe "update role" do
    setup [:create_role]

    test "renders role when data is valid", %{conn: conn, role: %Role{id: id} = role} do
      conn = put(conn, Routes.role_path(conn, :update, role), role: @update_attrs)
      assert %{"id" => ^id} = json_response(conn, 200)["data"]

      conn = get(conn, Routes.role_path(conn, :show, id))

      assert %{
               "id" => id,
               "can_add_users" => false,
               "can_remove_users" => false,
               "can_update_server_description" => false,
               "can_update_server_name" => false,
               "can_update_server_privacy" => false,
               "name" => "some updated name"
             } = json_response(conn, 200)["data"]
    end

    test "renders errors when data is invalid", %{conn: conn, role: role} do
      conn = put(conn, Routes.role_path(conn, :update, role), role: @invalid_attrs)
      assert json_response(conn, 422)["errors"] != %{}
    end
  end

  describe "delete role" do
    setup [:create_role]

    test "deletes chosen role", %{conn: conn, role: role} do
      conn = delete(conn, Routes.role_path(conn, :delete, role))
      assert response(conn, 204)

      assert_error_sent 404, fn ->
        get(conn, Routes.role_path(conn, :show, role))
      end
    end
  end

  defp create_role(_) do
    role = fixture(:role)
    %{role: role}
  end
end
