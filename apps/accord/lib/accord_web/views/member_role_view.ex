defmodule AccordWeb.MemberRoleView do
  use AccordWeb, :view
  alias AccordWeb.MemberRoleView

  def render("index.json", %{member_role: member_role}) do
    %{data: render_many(member_role, MemberRoleView, "member_role.json")}
  end

  def render("show.json", %{member_role: member_role}) do
    %{data: render_one(member_role, MemberRoleView, "member_role.json")}
  end

  def render("member_role.json", %{member_role: member_role}) do
    %{id: member_role.id}
  end
end
