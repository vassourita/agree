defmodule AccordWeb.MemberRoleControllerTest do
  use AccordWeb.ConnCase

  alias Accord.Roles
  alias Accord.Roles.MemberRole

  @create_attrs %{

  }
  @update_attrs %{

  }
  @invalid_attrs %{}

  def fixture(:member_role) do
    {:ok, member_role} = Roles.create_member_role(@create_attrs)
    member_role
  end

  setup %{conn: conn} do
    {:ok, conn: put_req_header(conn, "accept", "application/json")}
  end

  describe "index" do
    test "lists all member_role", %{conn: conn} do
      conn = get(conn, Routes.member_role_path(conn, :index))
      assert json_response(conn, 200)["data"] == []
    end
  end

  describe "create member_role" do
    test "renders member_role when data is valid", %{conn: conn} do
      conn = post(conn, Routes.member_role_path(conn, :create), member_role: @create_attrs)
      assert %{"id" => id} = json_response(conn, 201)["data"]

      conn = get(conn, Routes.member_role_path(conn, :show, id))

      assert %{
               "id" => id
             } = json_response(conn, 200)["data"]
    end

    test "renders errors when data is invalid", %{conn: conn} do
      conn = post(conn, Routes.member_role_path(conn, :create), member_role: @invalid_attrs)
      assert json_response(conn, 422)["errors"] != %{}
    end
  end

  describe "update member_role" do
    setup [:create_member_role]

    test "renders member_role when data is valid", %{conn: conn, member_role: %MemberRole{id: id} = member_role} do
      conn = put(conn, Routes.member_role_path(conn, :update, member_role), member_role: @update_attrs)
      assert %{"id" => ^id} = json_response(conn, 200)["data"]

      conn = get(conn, Routes.member_role_path(conn, :show, id))

      assert %{
               "id" => id
             } = json_response(conn, 200)["data"]
    end

    test "renders errors when data is invalid", %{conn: conn, member_role: member_role} do
      conn = put(conn, Routes.member_role_path(conn, :update, member_role), member_role: @invalid_attrs)
      assert json_response(conn, 422)["errors"] != %{}
    end
  end

  describe "delete member_role" do
    setup [:create_member_role]

    test "deletes chosen member_role", %{conn: conn, member_role: member_role} do
      conn = delete(conn, Routes.member_role_path(conn, :delete, member_role))
      assert response(conn, 204)

      assert_error_sent 404, fn ->
        get(conn, Routes.member_role_path(conn, :show, member_role))
      end
    end
  end

  defp create_member_role(_) do
    member_role = fixture(:member_role)
    %{member_role: member_role}
  end
end
