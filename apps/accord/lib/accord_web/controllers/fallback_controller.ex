defmodule AccordWeb.FallbackController do
  use AccordWeb, :controller

  def call(conn, {:error, %Ecto.Changeset{} = changeset}) do
    conn
    |> put_status(:bad_request)
    |> put_view(AccordWeb.ErrorView)
    |> render("400.json", result: changeset)
  end

  def call(conn, {:error, %{reason: :not_found, resource_name: resource_name}}) do
    conn
    |> put_status(:not_found)
    |> put_view(AccordWeb.ErrorView)
    |> render("404.json", result: resource_name)
  end

  def call(conn, {:error, :internal_server_error}) do
    conn
    |> put_status(:internal_server_error)
    |> put_view(AccordWeb.ErrorView)
    |> render("500.json")
  end

  def call(conn, _) do
    conn
    |> put_status(:internal_server_error)
    |> put_view(AccordWeb.ErrorView)
    |> render("500.json")
  end
end
