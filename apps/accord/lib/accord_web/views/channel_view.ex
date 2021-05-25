defmodule AccordWeb.ChannelView do
  use AccordWeb, :view
  alias AccordWeb.ChannelView

  def render("index.json", %{channel: channel}) do
    %{data: render_many(channel, ChannelView, "channel.json")}
  end

  def render("show.json", %{channel: channel}) do
    %{data: render_one(channel, ChannelView, "channel.json")}
  end

  def render("channel.json", %{channel: channel}) do
    %{id: channel.id, name: channel.name}
  end
end
