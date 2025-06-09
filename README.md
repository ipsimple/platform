# IpSimple - A Simple, Reliable, and Scalable Public IP Address API

IpSimple is a straightforward public IP address API designed for seamless integration into any application. Whether you're a developer, IT professional, or a business looking to enhance your network management capabilities, IpSimple provides a reliable and scalable solution for obtaining public IP addresses effortlessly.

## Key Features

- **Unlimited Usage**: No restrictions on the number of requests you can make. IpSimple can handle anything from a few requests per day to millions per minute.
- **Seamless Compatibility**: Supports both IPv4 and IPv6 addresses, ensuring compatibility with all modern network configurations and technologies.
- **High Availability**: Hosted on Microsoft Azure with a multi-zone and multi-region setup, guaranteeing high availability and reliability.
- **Open Source Transparency**: Completely open source, promoting transparency and community contributions.
- **Privacy-Focused**: No visitor information is ever logged, ensuring complete anonymity and security.
- **Future-Proof and Reliable**: Committed to maintaining and improving the service indefinitely.

## Use Cases

- **Network Management**: Obtain public IP addresses programmatically to streamline network management tasks.
- **Cloud Infrastructure**: Provision new cloud servers with ease by automating the retrieval of public IP addresses.
- **Security Applications**: Enhance security systems by integrating accurate and up-to-date public IP address information.
- **Developer Tools**: Build powerful tools and applications requiring public IP address information with minimal setup and effort.

## Getting Started

IpSimple provides a simple REST API for retrieving IP address information. You can use it directly via HTTP requests or integrate it into your applications.

### Live API Usage

The API is hosted at `https://api.ipsimple.org` and provides several endpoints:

```bash
# Get complete IP information (JSON)
curl https://api.ipsimple.org/api/ip

# Get only IPv4 address
curl https://api.ipsimple.org/api/ip/v4

# Get only IPv6 address  
curl https://api.ipsimple.org/api/ip/v6

# Get all detected addresses
curl https://api.ipsimple.org/api/ip/all
```

### Response Format

The API returns JSON responses with the following structure:

```json
{
  "ipv4": "203.0.113.1",
  "ipv6": "2001:db8::1",
  "allAddresses": [
    "203.0.113.1",
    "2001:db8::1"
  ]
}
```

### Integration Examples

**JavaScript/Node.js:**
```javascript
fetch('https://api.ipsimple.org/api/ip')
  .then(response => response.json())
  .then(data => console.log('Your IP:', data.ipv4));
```

**Python:**
```python
import requests
response = requests.get('https://api.ipsimple.org/api/ip')
ip_info = response.json()
print(f"Your IP: {ip_info['ipv4']}")
```

**C#:**
```csharp
using var client = new HttpClient();
var response = await client.GetStringAsync("https://api.ipsimple.org/api/ip");
var ipInfo = JsonSerializer.Deserialize<IpResponse>(response);
Console.WriteLine($"Your IP: {ipInfo.Ipv4}");
```

## Installation & Development Setup

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [Docker](https://docs.docker.com/get-docker/) (optional, for containerized development)
- [Git](https://git-scm.com/) for version control
- A code editor like [Visual Studio Code](https://code.visualstudio.com/), [Visual Studio](https://visualstudio.microsoft.com/), or [JetBrains Rider](https://www.jetbrains.com/rider/)

### Local Development Setup

1. **Clone the repository:**
    ```bash
    git clone https://github.com/ipsimple/platform.git
    cd platform
    ```

2. **Restore dependencies:**
    ```bash
    dotnet restore src/IpSimple.Platform.sln
    ```

3. **Build the solution:**
    ```bash
    dotnet build src/IpSimple.Platform.sln --configuration Release
    ```

4. **Run tests:**
    ```bash
    dotnet test src/IpSimple.Platform.sln
    ```

5. **Run the API locally:**
    ```bash
    cd src/IpSimple.PublicIp.Api
    dotnet run
    ```
    
    The API will be available at `https://localhost:5001` or `http://localhost:5000`

### Using Docker (Alternative)

1. **Build the Docker image:**
    ```bash
    docker build -f src/IpSimple.PublicIp.Api/Dockerfile -t ipsimple-api src/
    ```

2. **Run the container:**
    ```bash
    docker run -p 8080:8080 ipsimple-api
    ```
    
    The API will be available at `http://localhost:8080`

### API Endpoints

Once running, you can test the following endpoints:

- `GET /api/ip` - Get all IP address information in JSON format
- `GET /api/ip/v4` - Get IPv4 address only
- `GET /api/ip/v6` - Get IPv6 address only  
- `GET /api/ip/all` - Get all detected IP addresses

### Testing the API

```bash
# Test locally running API
curl http://localhost:5000/api/ip

# Test with specific format
curl http://localhost:5000/api/ip/v4
```

## Project Structure

The IpSimple platform is built with .NET 9.0 and follows a clean architecture pattern:

```
src/
├── IpSimple.PublicIp.Api/          # Main API application
├── IpSimple.Domain/                # Domain models and constants
├── IpSimple.Extensions/            # HTTP context extensions for IP parsing
├── IpSimple.Domain.Tests/          # Domain layer tests
├── IpSimple.Extensions.Tests/      # Extensions layer tests
├── IpSimple.PublicIp.Api.Tests/    # API layer tests
└── IpSimple.PublicIp.Api.BenchmarkTesting/  # Performance benchmarks
```

### Key Components

- **API Layer**: ASP.NET Core Web API with minimal APIs
- **Domain Layer**: Core business logic and models
- **Extensions Layer**: HTTP context utilities for IP address extraction
- **Performance Optimized**: Designed to handle 1000+ requests per second
- **Comprehensive Testing**: Unit tests and benchmark testing included

## Performance

This API is optimized for high-throughput scenarios:

- **Target Performance**: 1000+ requests per second
- **Low Memory Usage**: Minimal allocation patterns
- **Efficient Parsing**: Optimized IP address extraction and validation
- **Scalable Architecture**: Designed for cloud deployment

You can run performance benchmarks using:

```bash
cd src/IpSimple.PublicIp.Api.BenchmarkTesting
dotnet run -c Release
```

## Contributing

We welcome contributions from the community! This project follows standard .NET development practices.

### Development Workflow

1. **Fork the repository** on GitHub
2. **Clone your fork locally:**
    ```bash
    git clone https://github.com/your-username/platform.git
    cd platform
    ```
3. **Create a feature branch:**
    ```bash
    git checkout -b feature/your-feature-name
    ```
4. **Set up the development environment:**
    ```bash
    dotnet restore src/IpSimple.Platform.sln
    dotnet build src/IpSimple.Platform.sln
    ```
5. **Make your changes** and add tests
6. **Run tests to ensure everything works:**
    ```bash
    dotnet test src/IpSimple.Platform.sln
    ```
7. **Run performance benchmarks** (if applicable):
    ```bash
    cd src/IpSimple.PublicIp.Api.BenchmarkTesting
    dotnet run -c Release
    ```
8. **Commit your changes:**
    ```bash
    git add .
    git commit -m "Add: your feature description"
    ```
9. **Push to your fork:**
    ```bash
    git push origin feature/your-feature-name
    ```
10. **Create a Pull Request** on GitHub

### Code Style Guidelines

- Follow standard C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Include unit tests for new functionality
- Ensure high performance (target: 1000+ RPS)
- Keep allocations minimal for hot paths

### Testing

Before submitting a PR, ensure:
- All unit tests pass: `dotnet test`
- Code builds without warnings: `dotnet build`
- Performance benchmarks don't regress significantly

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any inquiries or feedback, please raise an issue here https://github.com/ipsimple/platform/issues
