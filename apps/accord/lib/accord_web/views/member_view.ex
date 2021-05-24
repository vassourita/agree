defmodule AccordWeb.MemberView do
  use AccordWeb, :view
  alias AccordWeb.MemberView

  def render("index.json", %{member: member}) do
    %{data: render_many(member, MemberView, "member.json")}
  end

  def render("show.json", %{member: member}) do
    %{data: render_one(member, MemberView, "member.json")}
  end

  def render("member.json", %{member: member}) do
    %{id: member.id,
      id: member.id}
  end
end
