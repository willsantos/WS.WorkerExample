# Worker Example
Projeto para fazer testes com um worker.

## itens chaves da app
Foi configurado o Serilog para enviar os logs diretamente para o loki, a url está hardcoded até o momento.


## Deploy
o docker-compose.yml está configurado pra subir a aplicação especificando uma imagem e a versão, o banco de dados, 
grafana e loki.


## Configuração do Grafana
Exemplo de uma dashboard para consultar os logs.

```json

{
  "annotations": {
    "list": [
      {
        "builtIn": 1,
        "datasource": {
          "type": "grafana",
          "uid": "-- Grafana --"
        },
        "enable": true,
        "hide": true,
        "iconColor": "rgba(0, 211, 255, 1)",
        "name": "Annotations & Alerts",
        "type": "dashboard"
      }
    ]
  },
  "editable": true,
  "fiscalYearStartMonth": 0,
  "graphTooltip": 0,
  "id": 6,
  "links": [],
  "panels": [
    {
      "datasource": {
        "default": false,
        "type": "loki",
        "uid": "adw7py7jsi4n4c"
      },
      "gridPos": {
        "h": 17,
        "w": 24,
        "x": 0,
        "y": 0
      },
      "id": 1,
      "options": {
        "dedupStrategy": "none",
        "enableLogDetails": true,
        "prettifyLogMessage": true,
        "showCommonLabels": false,
        "showLabels": false,
        "showTime": true,
        "sortOrder": "Descending",
        "wrapLogMessage": true
      },
      "targets": [
        {
          "datasource": {
            "type": "loki",
            "uid": "adw7py7jsi4n4c"
          },
          "editorMode": "code",
          "expr": "{app=\"workerExample\"} |= \"$search\" ",
          "queryType": "range",
          "refId": "A"
        }
      ],
      "title": "Logs do Serilog",
      "type": "logs"
    }
  ],
  "schemaVersion": 39,
  "tags": [],
  "templating": {
    "list": [
      {
        "current": {
          "isNone": true,
          "selected": false,
          "text": "None",
          "value": ""
        },
        "datasource": {
          "type": "prometheus",
          "uid": "fdw6t3gkogq2oa"
        },
        "definition": "",
        "hide": 0,
        "includeAll": false,
        "label": "Busca logs",
        "multi": false,
        "name": "search",
        "options": [],
        "query": "",
        "refresh": 1,
        "regex": "",
        "skipUrlSync": false,
        "sort": 0,
        "type": "query"
      }
    ]
  },
  "time": {
    "from": "now-6h",
    "to": "now"
  },
  "timepicker": {},
  "timezone": "America/Sao_Paulo",
  "title": "Logs Loki",
  "uid": "adwb8xsdm7o5cd",
  "version": 3,
  "weekStart": ""
}

```