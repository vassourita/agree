defmodule AccordWeb.ChannelControllerTest do
  use AccordWeb.ConnCase

  alias Accord.Servers
  alias Accord.Servers.Channel

  @create_attrs %{
    name: "some name"
  }
  @update_attrs %{
    name: "some updated name"
  }
  @invalid_attrs %{name: nil}

  def fixture(:channel) do
    {:ok, channel} = Servers.create_channel(@create_attrs)
    channel
  end

  setup %{conn: conn} do
    {:ok, conn: put_req_header(conn, "accept", "application/json")}
  end

  describe "index" do
    test "lists all channel", %{conn: conn} do
      conn = get(conn, Routes.channel_path(conn, :index))
      assert json_response(conn, 200)["data"] == []
    end
  end

  describe "create channel" do
    test "renders channel when data is valid", %{conn: conn} do
      conn = post(conn, Routes.channel_path(conn, :create), channel: @create_attrs)
      assert %{"id" => id} = json_response(conn, 201)["data"]

      conn = get(conn, Routes.channel_path(conn, :show, id))

      assert %{
               "id" => id,
               "name" => "some name"
             } = json_response(conn, 200)["data"]
    end

    test "renders errors when data is invalid", %{conn: conn} do
      conn = post(conn, Routes.channel_path(conn, :create), channel: @invalid_attrs)
      assert json_response(conn, 422)["errors"] != %{}
    end
  end

  describe "update channel" do
    setup [:create_channel]

    test "renders channel when data is valid", %{conn: conn, channel: %Channel{id: id} = channel} do
      conn = put(conn, Routes.channel_path(conn, :update, channel), channel: @update_attrs)
      assert %{"id" => ^id} = json_response(conn, 200)["data"]

      conn = get(conn, Routes.channel_path(conn, :show, id))

      assert %{
               "id" => id,
               "name" => "some updated name"
             } = json_response(conn, 200)["data"]
    end

    test "renders errors when data is invalid", %{conn: conn, channel: channel} do
      conn = put(conn, Routes.channel_path(conn, :update, channel), channel: @invalid_attrs)
      assert json_response(conn, 422)["errors"] != %{}
    end
  end

  describe "delete channel" do
    setup [:create_channel]

    test "deletes chosen channel", %{conn: conn, channel: channel} do
      conn = delete(conn, Routes.channel_path(conn, :delete, channel))
      assert response(conn, 204)

      assert_error_sent 404, fn ->
        get(conn, Routes.channel_path(conn, :show, channel))
      end
    end
  end

  defp create_channel(_) do
    channel = fixture(:channel)
    %{channel: channel}
  end
end
