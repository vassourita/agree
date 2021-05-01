defmodule AccordWeb.FallbackController do
  use AccordWeb, :controller

  def call(conn, {:error, result}) do
    conn
    |> put_status(:bad_request)
    |> put_view(AccordWeb.ErrorView)
    |> render("400.json", result: result)
  end
end
