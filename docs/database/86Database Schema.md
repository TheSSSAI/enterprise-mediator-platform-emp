# 1 Title

Enterprise Mediator Platform - Observability Database

# 2 Name

emp_observability_db

# 3 Db Type

- search
- timeseries

# 4 Db Technology

OpenSearch

# 5 Entities

- {'name': 'AuditLog', 'description': 'Immutable, time-series log of all critical system actions for security, compliance, and traceability. REQ-DAT-001 & REQ-FUN-005. Offloaded to a dedicated search/log database for performance.', 'attributes': [{'name': 'auditLogId', 'type': 'Guid', 'isRequired': True, 'isPrimaryKey': True, 'size': 0, 'isUnique': True, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'actorUserId', 'type': 'Guid', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'ipAddress', 'type': 'VARCHAR', 'isRequired': False, 'isPrimaryKey': False, 'size': 45, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'userAgent', 'type': 'TEXT', 'isRequired': False, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'actionType', 'type': 'VARCHAR', 'isRequired': True, 'isPrimaryKey': False, 'size': 50, 'isUnique': False, 'constraints': ["CHECK (actionType IN ('CREATE', 'UPDATE', 'DELETE', 'LOGIN', 'LOGOUT', 'OVERRIDE'))"], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'entityName', 'type': 'VARCHAR', 'isRequired': True, 'isPrimaryKey': False, 'size': 100, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'entityId', 'type': 'VARCHAR', 'isRequired': True, 'isPrimaryKey': False, 'size': 36, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'changes', 'type': 'JSON', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'createdAt', 'type': 'DateTimeOffset', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': ['DEFAULT CURRENT_TIMESTAMP'], 'precision': 0, 'scale': 0, 'isForeignKey': False}], 'primaryKeys': ['auditLogId'], 'uniqueConstraints': [], 'indexes': [{'name': 'IX_AuditLog_Entity', 'columns': ['entityName', 'entityId'], 'type': 'Keyword'}, {'name': 'IX_AuditLog_Actor', 'columns': ['actorUserId'], 'type': 'Keyword'}, {'name': 'IX_AuditLog_CreatedAt_EntityName', 'columns': ['createdAt', 'entityName'], 'type': 'Date and Keyword'}]}

