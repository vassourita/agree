defmodule AccordWeb.ViewHelpers do
  @spec put_assoc(map(), boolean, atom(), function()) :: map()
  def put_assoc(view, true, assoc_name, render_assoc_fn) do
    view
    |> Map.put(
      assoc_name,
      render_assoc_fn.()
    )
  end

  def put_assoc(view, false, _assoc_name, _render_assoc_fn), do: view
end
