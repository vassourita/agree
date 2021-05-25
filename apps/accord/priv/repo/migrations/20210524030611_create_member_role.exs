defmodule Accord.Repo.Migrations.CreateMemberRole do
  use Ecto.Migration

  def change do
    create table(:member_role, primary_key: false) do
      add :role_id,
        references(:role, on_delete: :delete_all, type: :binary_id),
        primary_key: true
      add :member_id,
        references(:member, on_delete: :delete_all, type: :binary_id),
        primary_key: true

      timestamps()
    end
  end
end
