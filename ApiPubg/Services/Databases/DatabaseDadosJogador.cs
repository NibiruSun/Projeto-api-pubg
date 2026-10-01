using ApiPubg.Models.ServicesModel;
using SQLite;

namespace ApiPubg.Services.Databases
{
    public class DatabaseDadosJogador
    {
        private readonly SQLiteAsyncConnection _db;
        public DatabaseDadosJogador(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);

            _db.CreateTableAsync<JogadorModel>();
            _db.CreateTableAsync<DadosJogadorModel>();
            _db.CreateTableAsync<TelemetryVictimDerrubou>();
            _db.CreateTableAsync<TelemetryVictimFinalizou>();
            _db.CreateTableAsync<TelemetryDerrubou>();
            _db.CreateTableAsync<TelemetryFinalizou>();
            _db.CreateTableAsync<DataHoraControle>();
        }

        #region Jogador
        public Task<int> AddJogadorAsync(JogadorModel jogador)
        {
            return _db.InsertAsync(jogador);
        }
        public Task<List<JogadorModel>> GetJogadorAsync()
        {
            return _db.Table<JogadorModel>().ToListAsync();
        }
        public Task<int> UpdateJogadorAsync(JogadorModel jogador)
        {
            return _db.UpdateAsync(jogador);
        }
        public Task<int> DeleteJogadorAsync(JogadorModel jogador)
        {
            return _db.DeleteAsync(jogador);
        }

        public Task<int> AddDadosJogadorAsync(DadosJogadorModel Dados)
        {
            return _db.InsertAsync(Dados);
        }
        public Task<List<DadosJogadorModel>> GetDadosJogadorAsync()
        {
            return _db.Table<DadosJogadorModel>().ToListAsync();
        }
        public Task<int> UpdateDadosJogadorAsync(DadosJogadorModel Dados)
        {
            return _db.UpdateAsync(Dados);
        }
        public Task<int> DeleteDadosJogadorAsync(DadosJogadorModel Dados)
        {
            return _db.DeleteAsync(Dados);
        }
        #endregion

        #region Vicitm
        public Task<int> AddTelemetryVictimDerrubouAsync(TelemetryVictimDerrubou telemetry)
        {
            return _db.InsertAsync(telemetry);
        }

        public Task<List<TelemetryVictimDerrubou>> GetTelemetryVictimDerrubouAsync()
        {
            return _db.Table<TelemetryVictimDerrubou>().ToListAsync();
        }

        public Task<int> DeleteTelemetryVictimDerrubouAsync(TelemetryVictimDerrubou telemetry)
        {
            return _db.DeleteAsync(telemetry);
        }

        public Task<int> AddTelemetryVictimFinalizouAsync(TelemetryVictimFinalizou telemetry)
        {
            return _db.InsertAsync(telemetry);
        }

        public Task<List<TelemetryVictimFinalizou>> GetTelemetryVictimFinalizouAsync()
        {
            return _db.Table<TelemetryVictimFinalizou>().ToListAsync();
        }

        public Task<int> DeleteTelemetryVictimFinalizouAsync(TelemetryVictimFinalizou telemetry)
        {
            return _db.DeleteAsync(telemetry);
        }

        #endregion

        #region Derrubou Finalizou

        public Task<int> AddTelemetryDerrubouAsync(TelemetryDerrubou telemetry)
        {
            return _db.InsertAsync(telemetry);
        }

        public Task<List<TelemetryDerrubou>> GetTelemetryDerrubouAsync()
        {
            return _db.Table<TelemetryDerrubou>().ToListAsync();
        }

        public Task<int> DeleteTelemetryDerrubouAsync(TelemetryDerrubou telemetry)
        {
            return _db.DeleteAsync(telemetry);
        }

        public Task<int> AddTelemetryFinalizouAsync(TelemetryFinalizou telemetry)
        {
            return _db.InsertAsync(telemetry);
        }

        public Task<List<TelemetryFinalizou>> GetTelemetryFinalizouAsync()
        {
            return _db.Table<TelemetryFinalizou>().ToListAsync();
        }

        public Task<int> DeleteTelemetryFinalizouAsync(TelemetryFinalizou telemetry)
        {
            return _db.DeleteAsync(telemetry);
        }

        #endregion

        #region DataHoraControle
        public Task<int> AddDataHoraControleAsync(DataHoraControle dataHora)
        {
            return _db.InsertAsync(dataHora);
        }

        public Task<List<DataHoraControle>> GetDataHoraControleAsync()
        {
            return _db.Table<DataHoraControle>().ToListAsync();
        }

        public Task<int> DeleleDataHoraControleAsync(DataHoraControle dataHora)
        {
            return _db.DeleteAsync(dataHora);
        }
        #endregion
    }
}
