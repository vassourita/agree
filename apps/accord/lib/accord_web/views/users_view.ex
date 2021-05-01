defmodule AccordWeb.UsersView do
  use AccordWeb, :view
  alias Accord.User

  def render("create.json", %{user: %User{} = user}) do
    %{
      message: "User created succesfully",
      user: render_one(user, __MODULE__, "user.json", as: :user)
    }
  end

  def render("user.json", %{user: %User{} = user}) do
    %{
      id: user.id,
      user_name: user.user_name,
      tag: String.pad_leading(Integer.to_string(user.tag), 4, "0"),
      email: user.email
    }
  end
end
