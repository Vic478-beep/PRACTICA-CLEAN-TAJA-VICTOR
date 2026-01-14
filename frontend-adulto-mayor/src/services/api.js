/**
 * Patrón Singleton para la conexión API.
 * Puerto configurado: 7083 (HTTPS)
 */
class ApiService {
  static instance = null;

  constructor() {
    if (ApiService.instance) {
      throw new Error("Ya existe una instancia. Usa ApiService.getInstance()");
    }
    
    // URL Base según tu configuración de backend .NET
    this.baseUrl = "https://localhost:7083/api"; 
    
    this.headers = {
      "Content-Type": "application/json",
      "Accept": "application/json"
    };
  }

  static getInstance() {
    if (!ApiService.instance) {
      ApiService.instance = new ApiService();
    }
    return ApiService.instance;
  }

  async get(endpoint) {
    try {
      const response = await fetch(`${this.baseUrl}${endpoint}`, {
        method: 'GET',
        headers: this.headers
      });
      return await this._handleResponse(response);
    } catch (error) {
      console.error(`Error GET ${endpoint}:`, error);
      throw error;
    }
  }

  async post(endpoint, data) {
    try {
      const response = await fetch(`${this.baseUrl}${endpoint}`, {
        method: 'POST',
        headers: this.headers,
        body: JSON.stringify(data)
      });
      return await this._handleResponse(response);
    } catch (error) {
      console.error(`Error POST ${endpoint}:`, error);
      throw error;
    }
  }

  async put(endpoint, data) {
    try {
      const response = await fetch(`${this.baseUrl}${endpoint}`, {
        method: 'PUT',
        headers: this.headers,
        body: JSON.stringify(data)
      });
      return await this._handleResponse(response);
    } catch (error) {
      console.error(`Error PUT ${endpoint}:`, error);
      throw error;
    }
  }

  async delete(endpoint) {
    try {
      const response = await fetch(`${this.baseUrl}${endpoint}`, {
        method: 'DELETE',
        headers: this.headers
      });
      if (response.status === 204) return true;
      return await this._handleResponse(response);
    } catch (error) {
      console.error(`Error DELETE ${endpoint}:`, error);
      throw error;
    }
  }

  async _handleResponse(response) {
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Error API (${response.status}): ${errorText || response.statusText}`);
    }
    const text = await response.text();
    return text ? JSON.parse(text) : {};
  }
}

export default ApiService;