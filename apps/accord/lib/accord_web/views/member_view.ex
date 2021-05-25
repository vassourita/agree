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
    %{
      id: member.id,
      allow_id: member.allow_user_id
    }
    |> Map.put_new(
      :roles,
      render_many(member.roles, AccordWeb.RoleView, "role.json", as: :role)
    )
  end
end
