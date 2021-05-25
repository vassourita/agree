defmodule Accord.AllowIntegration.User do
  def authenticate_from_token(token) do
    me_endpoint = "#{System.get_env("ALLOW_BASEURL")}/accounts/@me"
    fetch_user(me_endpoint, token)
  end

  def get_user(token, id) do
    user_endpoint = "#{System.get_env("ALLOW_BASEURL")}/accounts/#{id}"
    fetch_user(user_endpoint, token)
  end

  defp fetch_user(endpoint, token) do
    allow_externaltoken = System.get_env("ALLOW_EXTERNALTOKEN")
    headers = [agreeallow_accesstoken: token, agreeallow_externaltoken: allow_externaltoken]

    case HTTPoison.get(endpoint, headers) do
      {:ok, %HTTPoison.Response{status_code: 200, body: body}} ->
        case Jason.decode(body) do
          {:ok, decoded_body} ->
            {:ok, decoded_body["user"]}

          _ ->
            {:error, "Something went wrong"}
        end

      _ ->
        {:error, "Something went wrong"}
    end
  end
end
