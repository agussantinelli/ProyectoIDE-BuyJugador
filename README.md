<h1>🛒 ProyectoIDE - BuyJugador</h1>

<div align="center">
    <a href="https://github.com/agussantinelli/ProyectoIDE-BuyJugador" target="_blank" style="text-decoration: none;">
        <img src="https://img.shields.io/badge/💻%20Repo%20Principal-BuyJugador-0b7285?style=for-the-badge&logo=github&logoColor=white" alt="Repo BuyJugador"/>
    </a>
    <a href="https://drive.google.com/drive/folders/1z1zvg535spSoWh4M8KYAg8vvq3pXs1fn?usp=sharing" target="_blank" style="text-decoration: none;">
        <img src="https://img.shields.io/badge/📂%20Carpeta%20del%20Proyecto-Google%20Drive-34a853?style=for-the-badge&logo=googledrive&logoColor=white" alt="Carpeta del proyecto"/>
    </a>
</div>

<p align="center">
    <a href="https://github.com/agussantinelli" target="_blank" style="text-decoration: none;">
        <img src="https://img.shields.io/badge/👤%20Agustín%20Santinelli-agussantinelli-000000?style=for-the-badge&logo=github&logoColor=white" alt="Agus"/>
    </a>
    <a href="https://github.com/martin-ratti" target="_blank" style="text-decoration: none;">
        <img src="https://img.shields.io/badge/👤%20Martín%20Ratti-martin--ratti-000000?style=for-the-badge&logo=github&logoColor=white" alt="Martín"/>
    </a>
    <a href="https://github.com/tomy19012005" target="_blank" style="text-decoration: none;">
        <img src="https://img.shields.io/badge/👤%20Tomás%20Levrand-tomy19012005-000000?style=for-the-badge&logo=github&logoColor=white" alt="Tomás"/>
    </a>
</p>

<p align="center">
    <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET Badge"/>
    <img src="https://img.shields.io/badge/Web-Blazor%20WebAssembly-512BD4?style=for-the-badge&logo=blazor&logoColor=white" alt="Blazor Badge"/>
    <img src="https://img.shields.io/badge/Desktop-WinForms-0078D4?style=for-the-badge&logo=windows&logoColor=white" alt="WinForms Badge"/>
    <img src="https://img.shields.io/badge/API-ASP.NET%20Core-512BD4?style=for-the-badge&logo=asp.net&logoColor=white" alt="ASP.NET Core Badge"/>
    <img src="https://img.shields.io/badge/DB-SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server Badge"/>
    <img src="https://img.shields.io/badge/Reports-PDFsharp%20%7C%20ScottPlot-0b7285?style=for-the-badge" alt="Reports Badge"/>
    <img src="https://img.shields.io/badge/Auth-JWT%20Bearer-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" alt="JWT Badge"/>
</p>

<hr>

<h2>🎯 Objetivo</h2>

<p>
    <strong>BuyJugador</strong> es un sistema de <strong>inventario y gestión de ventas</strong> orientado a un local de productos de gaming y componentes electrónicos.
    Permite administrar productos, gestionar ventas y pedidos a proveedores, controlar stock y generar reportes de negocio.
</p>

<p>
    Es un trabajo práctico de la cátedra <strong>IDE</strong> (UTN-FRRO) desarrollado en equipo, con foco en:
</p>

<ul>
    <li>Arquitectura por capas y separación clara de responsabilidades.</li>
    <li>Escenario <strong>multi-cliente</strong>: aplicación web (Blazor) + escritorio (WinForms) usando la misma Web API.</li>
    <li>Buenas prácticas de diseño (DTOs, servicios de dominio, Repository + UnitOfWork, validaciones, JWT, etc.).</li>
</ul>

<hr>

<h2>🧱 Stack Tecnológico</h2>

<table>
 <thead>
  <tr>
   <th>Componente</th>
   <th>Tecnología</th>
   <th>Notas</th>
  </tr>
 </thead>
 <tbody>
  <tr>
   <td><strong>Cliente Web</strong></td>
   <td>Blazor WebAssembly (.NET 8)</td>
   <td>UI moderna SPA, Bootstrap Icons, Blazored.LocalStorage para sesión/tokens.</td>
  </tr>
  <tr>
   <td><strong>Cliente Desktop</strong></td>
   <td>Windows Forms (.NET 8)</td>
   <td>Interfaz clásica MDI, integración con ScottPlot para gráficos.</td>
  </tr>
  <tr>
   <td><strong>Web API</strong></td>
   <td>ASP.NET Core 8.0</td>
   <td>Minimal APIs, JWT Bearer, CORS, Swagger/OpenAPI.</td>
  </tr>
  <tr>
   <td><strong>Lógica de Dominio</strong></td>
   <td>Class Libraries .NET 8</td>
   <td>Servicios de dominio, validaciones, reglas de negocio, generación de reportes.</td>
  </tr>
  <tr>
   <td><strong>Acceso a Datos</strong></td>
   <td>Entity Framework Core 8.0</td>
   <td>SQL Server provider, Repository + UnitOfWork, filtros de consulta globales.</td>
  </tr>
  <tr>
   <td><strong>Base de Datos</strong></td>
   <td>Microsoft SQL Server</td>
   <td>Modelo relacional para productos, ventas, pedidos, proveedores, personas, geografía.</td>
  </tr>
  <tr>
   <td><strong>Reportes</strong></td>
   <td>PDFsharp 6.1.0 + ScottPlot 5.1.57</td>
   <td>PDFs con gráficos embebidos (historial de precios, ventas por vendedor).</td>
  </tr>
  <tr>
   <td><strong>Seguridad</strong></td>
   <td>JWT + BCrypt.Net</td>
   <td>Tokens JWT para autenticación; passwords hasheadas con BCrypt.</td>
  </tr>
 </tbody>
</table>

<hr>

<h2>🏛️ Arquitectura y Topología</h2>

<p>
    BuyJugador está organizado como una solución <strong>multi-proyecto</strong> con capas bien definidas:
</p>

<ul>
    <li><strong>BlazorApp</strong>: cliente web (Blazor WebAssembly).</li>
    <li><strong>WinForms</strong>: cliente de escritorio Windows Forms.</li>
    <li><strong>WebAPI</strong>: capa HTTP que expone endpoints REST.</li>
    <li><strong>ApiClient</strong>: clientes HTTP tipados reutilizables para Blazor y WinForms.</li>
    <li><strong>DominioServicios</strong>: servicios de dominio y lógica de negocio.</li>
    <li><strong>DominioModelo</strong>: entidades de dominio (Venta, Producto, Pedido, etc.).</li>
    <li><strong>Data</strong>: DbContext, repositorios, UnitOfWork, seeding.</li>
    <li><strong>DTOs</strong>: modelos de request/response para transporte entre capas.</li>
</ul>

<p><strong>Topología de ejecución (ports por defecto):</strong></p>
<ul>
    <li><strong>WebAPI</strong>: <code>https://localhost:7145</code></li>
    <li><strong>BlazorApp</strong>: <code>https://localhost:7035</code></li>
    <li><strong>WinForms</strong>: corre como app de escritorio, conectándose a la API en <code>7145</code>.</li>
</ul>

<p>
    La WebAPI utiliza una política de <strong>CORS</strong> que permite únicamente el origen del cliente Blazor.
</p>

<hr>

<h2>🧩 Dominios de Negocio</h2>

<p>Los principales dominios de negocio son:</p>

<ul>
    <li><strong>Ventas (Sales Domain)</strong>
        <ul>
            <li><code>Venta</code>, <code>LineaVenta</code>.</li>
            <li>Creación de ventas, decremento de stock, workflow de finalización (sólo ventas pendientes se pueden editar).</li>
        </ul>
    </li>
    <li><strong>Pedidos a Proveedores (Orders Domain)</strong>
        <ul>
            <li><code>Pedido</code>, <code>LineaPedido</code>.</li>
            <li>Creación de pedidos, incremento de stock cuando el pedido se marca como recibido.</li>
        </ul>
    </li>
    <li><strong>Productos e Inventario</strong>
        <ul>
            <li><code>Producto</code>, <code>TipoProducto</code>, stock mínimo, stock actual.</li>
            <li>Activación/desactivación (soft delete), consultas de bajo stock.</li>
        </ul>
    </li>
    <li><strong>Precios (Pricing Domain)</strong>
        <ul>
            <li><code>PrecioVenta</code>, <code>PrecioCompra</code>.</li>
            <li>Gestión temporal de precios, historial de cambios y consultas históricas.</li>
        </ul>
    </li>
    <li><strong>Proveedores y Relaciones</strong>
        <ul>
            <li><code>Proveedor</code>, <code>ProductoProveedor</code>.</li>
            <li>Relación productos ↔ proveedores, soft delete de proveedores.</li>
        </ul>
    </li>
    <li><strong>Personas y Geografía</strong>
        <ul>
            <li><code>Persona</code> (usuarios/vendedores), <code>Provincia</code>, <code>Localidad</code>.</li>
            <li>Datos geográficos obtenidos desde APIs públicas de Argentina (via DbSeeder en ambiente de desarrollo).</li>
        </ul>
    </li>
</ul>

<p><strong>Servicios de dominio clave (proyecto <code>DominioServicios</code>):</strong></p>

<ul>
    <li><code>VentaService</code> – gestión de ventas (creación, líneas, total del día).</li>
    <li><code>PedidoService</code> – gestión de pedidos (pendientes, marcarlos como recibidos).</li>
    <li><code>ProductoService</code> – CRUD de productos, stock y bajas lógicas.</li>
    <li><code>PrecioVentaService</code>, <code>PrecioCompraService</code> – manejo de historial de precios.</li>
    <li><code>PersonaService</code> – usuarios, autenticación, hashing de contraseñas con BCrypt.</li>
    <li><code>ProveedorService</code> – administración de proveedores.</li>
    <li><code>ReporteService</code> – generación de reportes PDF/PNG (ventas por vendedor, historial de precios).</li>
</ul>

<hr>

<h2>🖥️ Cliente Web (Blazor WebAssembly)</h2>

<ul>
    <li><strong>Router y Auth</strong>
        <ul>
            <li><code>App.razor</code>: router con <code>AuthorizeRouteView</code> para rutas protegidas.</li>
            <li><code>CustomAuthenticationStateProvider</code>: integra el estado de autenticación (JWT) con Blazor.</li>
        </ul>
    </li>
    <li><strong>Layout y Navegación</strong>
        <ul>
            <li><code>MainLayout.razor</code>: shell principal con sidebar colapsable y topbar.</li>
            <li>Menú lateral con opciones mostradas/ocultas según <strong>rol administrador</strong>.</li>
        </ul>
    </li>
    <li><strong>Dashboard</strong> – <code>Home.razor</code>
        <ul>
            <li>Muestra KPIs:
                <ul>
                    <li>Ventas de hoy.</li>
                    <li>Pedidos pendientes.</li>
                    <li>Productos con bajo stock.</li>
                </ul>
            </li>
            <li>Carga en paralelo usando <code>Task.WhenAll</code> para mejorar performance.</li>
        </ul>
    </li>
    <li><strong>Autenticación</strong> – <code>Login.razor</code>
        <ul>
            <li>Login con opción “Recordarme”.</li>
            <li>Tokens almacenados en <code>LocalStorage</code> o en sesión en memoria.</li>
        </ul>
    </li>
</ul>

<hr>

<h2>🪟 Cliente Desktop (WinForms)</h2>

<ul>
    <li><strong>Bootstrap de la aplicación</strong> – <code>Program.cs</code>
        <ul>
            <li>Usa <strong>HostBuilder</strong> e inyección de dependencias.</li>
            <li>Loop login → MainForm → logout (permite volver a iniciar sesión con otro usuario).</li>
        </ul>
    </li>
    <li><strong>MDI y navegación</strong> – <code>MainForm</code>
        <ul>
            <li>Formulario MDI contenedor con panel de menú dinámico.</li>
            <li>Botones/acciones visibles según rol (admin vs empleado).</li>
        </ul>
    </li>
    <li><strong>Reportes y gráficos</strong>
        <ul>
            <li>Formularios dedicados (por ejemplo, historial de precios, ventas por vendedor).</li>
            <li>Uso de <strong>ScottPlot</strong> para gráficos y exportación a PNG / PDF (vía <code>ReporteService</code>).</li>
        </ul>
    </li>
</ul>

<hr>

<h2>🌐 Capa Web API y Clientes HTTP</h2>

<h3>Configuración principal (WebAPI)</h3>

<ul>
    <li>Puerto por defecto: <code>https://localhost:7145</code>.</li>
    <li>Swagger habilitado en entorno de desarrollo (<code>/swagger</code>).</li>
    <li>JWT Bearer configurado con validación de issuer/audience y tiempo de vida.</li>
    <li>CORS configurado para permitir únicamente el origen del cliente Blazor.</li>
</ul>

<h3>ApiClient – Clientes HTTP tipados</h3>

<p>Ambos clientes (Blazor y WinForms) comparten el proyecto <strong>ApiClient</strong>:</p>

<ul>
    <li><code>VentaApiClient</code>, <code>PedidoApiClient</code>, <code>ProductoApiClient</code>, <code>PersonaApiClient</code>, <code>ProveedorApiClient</code>, <code>PrecioVentaApiClient</code>, <code>PrecioCompraApiClient</code>, <code>TipoProductoApiClient</code>, <code>ReporteApiClient</code>, etc.</li>
    <li><code>TokenMessageHandler</code>:
        <ul>
            <li>Intercepta requests HTTP.</li>
            <li>Inyecta el header <code>Authorization: Bearer &lt;token&gt;</code> leyendo desde LocalStorage o memoria.</li>
        </ul>
    </li>
    <li>Se registra un cliente “<strong>NoAuth</strong>” separado para el endpoint de login.</li>
</ul>

<hr>

<h2>🔐 Seguridad y Modelo de Estado</h2>

<h3>Autenticación</h3>

<ul>
    <li>Passwords de <code>Persona</code> almacenadas hasheadas con <strong>BCrypt.Net</strong>.</li>
    <li>Al loguearse, la WebAPI emite un <strong>JWT</strong> con claims de:
        <ul>
            <li>Id de persona.</li>
            <li>Email.</li>
            <li>Nombre/apellido.</li>
            <li>Rol (admin / empleado).</li>
        </ul>
    </li>
    <li>Los clientes guardan el token y lo envían en cada request protegida.</li>
</ul>

<h3>Autorización</h3>

<ul>
    <li><strong>A nivel endpoint</strong>: atributos <code>[Authorize]</code> y políticas en WebAPI.</li>
    <li><strong>A nivel UI</strong>:
        <ul>
            <li>Blazor: condicionales como <code>@if (_esAdmin)</code> para ocultar/mostrar opciones.</li>
            <li>WinForms: botones habilitados/deshabilitados según <code>UserSessionService.EsAdmin</code>.</li>
        </ul>
    </li>
    <li><strong>A nivel datos</strong>:
        <ul>
            <li>Soft delete en entidades como <code>Persona.Estado</code>, <code>Producto.Activo</code>, <code>Proveedor.Activo</code>.</li>
            <li>Filtros globales de EF Core para excluir registros inactivos por defecto.</li>
        </ul>
    </li>
</ul>

<h3>Patrones de workflow</h3>

<ul>
    <li><strong>Venta</strong>: crear venta (Pendiente) → agregar líneas → decrementar stock → finalizar (Finalizada, ya no editable).</li>
    <li><strong>Pedido</strong>: crear pedido (Pendiente) → agregar líneas → marcar como Recibido → incrementar stock.</li>
    <li><strong>Two-phase save</strong>: algunos servicios usan varias llamadas a <code>SaveChangesAsync()</code> dentro de transacciones para:
        <ul>
            <li>Generar Ids primero.</li>
            <li>Cargar entidades relacionadas.</li>
            <li>Aplicar lógicas de stock y totales.</li>
        </ul>
    </li>
</ul>

<hr>

<h2>📊 Módulo de Reportes</h2>

<p>El sistema incluye reportes especializados tanto en Blazor como en WinForms:</p>

<ul>
    <li><strong>Reporte de Ventas por Vendedor</strong>
        <ul>
            <li>Últimos 7 días para un vendedor seleccionado.</li>
            <li>Implementado con consultas SQL optimizadas (ADO.NET) en <code>ReporteRepository</code>.</li>
            <li>Salida: datos y/o documento PDF.</li>
        </ul>
    </li>
    <li><strong>Historial de Precios</strong>
        <ul>
            <li>Gráfico de línea con la evolución de precios en el tiempo.</li>
            <li>Soporta múltiples productos a la vez.</li>
            <li>Salida: imagen PNG y/o PDF con el gráfico embebido.</li>
        </ul>
    </li>
</ul>

<p><strong>Stack de generación de documentos:</strong></p>

<ul>
    <li><strong>ScottPlot</strong> – render de gráficos con eje DateTime.</li>
    <li><strong>PDFsharp</strong> – creación de PDFs, texto e imágenes embebidas.</li>
    <li><strong>Fuentes DejaVu Sans</strong> – embebidas para compatibilidad multiplataforma.</li>
</ul>

<hr>

<h2>🚀 Puesta en Marcha (Setup Local)</h2>

<h3>1. Requisitos</h3>

<ul>
    <li><a href="https://dotnet.microsoft.com/download" target="_blank">.NET SDK 8.0</a></li>
    <li>SQL Server (Express / LocalDB / instancia local).</li>
    <li>Visual Studio 2022 / Rider / VS Code + extensiones de C#.</li>
</ul>

<h3>2. Configuración básica</h3>

<ol>
    <li>Clonar el repositorio:
        <pre><code>git clone https://github.com/agussantinelli/ProyectoIDE-BuyJugador.git
cd ProyectoIDE-BuyJugador
</code></pre>
    </li>
    <li>Configurar la <strong>connection string</strong> en <code>appsettings.json</code> (proyecto WebAPI), por ejemplo:
        <pre><code>"ConnectionStrings": {
  "BuyJugadorConnection": "Server=.\\SQLEXPRESS;Database=BuyJugadorDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;"
}
</code></pre>
    </li>
    <li>Ejecutar migraciones o dejar que el <strong>DbSeeder</strong> configure los datos iniciales según la estrategia definida en el proyecto (personas demo, productos, datos geográficos, etc.).</li>
    <li>Levantar la WebAPI:
        <pre><code>cd WebAPI
dotnet run
</code></pre>
    </li>
    <li>Levantar el cliente Blazor:
        <pre><code>cd BlazorApp
dotnet run
</code></pre>
        Luego navegar a <code>https://localhost:7035</code>.
    </li>
    <li>Opcional: ejecutar el cliente WinForms desde el proyecto <code>WinForms</code> (por IDE o <code>dotnet run</code>) para la versión escritorio.</li>
</ol>

<p>
    Para detalles más finos de configuración, scripts SQL, capturas, etc., ver la carpeta de proyecto en:
    <a href="https://drive.google.com/drive/folders/1z1zvg535spSoWh4M8KYAg8vvq3pXs1fn?usp=sharing" target="_blank">
        Google Drive – ProyectoIDE BuyJugador
    </a>.
</p>

<hr>

<h2>👥 Equipo</h2>

<ul>
    <li><strong>Agustín Santinelli</strong> – <a href="https://github.com/agussantinelli" target="_blank">@agussantinelli</a></li>
    <li><strong>Martín Ratti</strong> – <a href="https://github.com/martin-ratti" target="_blank">@martin-ratti</a></li>
    <li><strong>Tomás Levrand</strong> – <a href="https://github.com/tomy19012005" target="_blank">@tomy19012005</a></li>
</ul>

<p>Proyecto académico desarrollado para la UTN FRRO (cátedra IDE).</p>

<hr>

<h2>🤝 Contribuir</h2>

<ol>
    <li>Haz un <strong>fork</strong> del repositorio.</li>
    <li>Crea una rama con el formato <code>feature/...</code> o <code>fix/...</code>.</li>
    <li>Realiza los cambios siguiendo el estilo del proyecto (capas, DTOs, servicios, etc.).</li>
    <li>Incluye tests o ejemplos de uso si agregás lógica de negocio relevante.</li>
    <li>Abre un <strong>Pull Request</strong> explicando claramente:
        <ul>
            <li>Qué problema resuelve o qué funcionalidad agrega.</li>
            <li>Qué capas toca (Blazor, WinForms, WebAPI, DominioServicios, Data, etc.).</li>
            <li>Si requiere cambios de BD o configuración.</li>
        </ul>
    </li>
</ol>

<hr>

<h2>⚖️ Licencia</h2>

<p>
    La licencia del proyecto se detalla en el archivo <code>LICENSE</code> de este repositorio (si corresponde).
</p>

<p><em>BuyJugador – Sistema multi-cliente de inventario y ventas para productos de gaming y componentes electrónicos.</em></p>
