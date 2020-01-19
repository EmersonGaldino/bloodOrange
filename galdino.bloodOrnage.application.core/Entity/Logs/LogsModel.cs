using System;

namespace galdino.bloodOrnage.application.core.Entity.Logs
{
    public class LogsModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Object { get; set; }

    }
}
