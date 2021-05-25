defmodule Accord.Repo.Migrations.CreateRole do
  use Ecto.Migration

  def change do
    create table(:role, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :name, :string, null: false
      add :can_update_server_name, :boolean, default: false, null: false
      add :can_update_server_description, :boolean, default: false, null: false
      add :can_update_server_privacy, :boolean, default: false, null: false
      add :can_add_users, :boolean, default: false, null: false
      add :can_remove_users, :boolean, default: false, null: false
      add :server_id, references(:server, on_delete: :delete_all, type: :binary_id), null: false

      timestamps()
    end

    create index(:role, [:server_id])
  end
end
