defmodule AccordWeb.ChannelController do
  use AccordWeb, :controller

  alias Accord.Servers
  alias Accord.Servers.Channel

  action_fallback AccordWeb.FallbackController

  def index(conn, %{category_id: category_id}) do
    channel = Servers.list_channel(category_id)
    render(conn, "index.json", channel: channel)
  end

  def create(conn, %{"channel" => channel_params}) do
    with {:ok, %Channel{} = channel} <- Servers.create_channel(channel_params) do
      conn
      |> put_status(:created)
      |> put_resp_header("location", Routes.channel_path(conn, :show, channel))
      |> render("show.json", channel: channel)
    end
  end

  def update(conn, %{"id" => id, "channel" => channel_params}) do
    channel = Servers.get_channel!(id)

    with {:ok, %Channel{} = channel} <- Servers.update_channel(channel, channel_params) do
      render(conn, "show.json", channel: channel)
    end
  end

  def delete(conn, %{"id" => id}) do
    channel = Servers.get_channel!(id)

    with {:ok, %Channel{}} <- Servers.delete_channel(channel) do
      send_resp(conn, :no_content, "")
    end
  end
end
