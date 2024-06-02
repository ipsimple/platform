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

Integrating IpSimple into your application is incredibly easy. Simply use one of our provided code samples or libraries, and you'll be up and running in no time. Whether you need the IP address in plain text, JSON, or JSONP format, IpSimple offers flexible options to suit your needs.

### Example Usage

```bash
# Retrieve your public IP address in JSON format
$ curl 'https://api.ipsimple.org?format=json'
```

## Installation

### Prerequisites

- Azure CLI
- Bicep CLI
- .NET 8.0 SDK
- Visual Studio Code or another IDE with Bicep and C# support
- An Azure account with an active subscription

### Setup

1. Clone the repository:
    ```bash
    git clone https://github.com/ipsimple/platform.git
    cd platform
    ```

2. Install dependencies:
    ```bash
    npm install
    ```

3. Start the development server:
    ```bash
    npm start
    ```

## Contributing

We welcome contributions from the community! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch:
    ```bash
    git checkout -b feature/YourFeature
    ```
3. Make your changes.
4. Commit your changes:
    ```bash
    git commit -m 'Add some feature'
    ```
5. Push to the branch:
    ```bash
    git push origin feature/YourFeature
    ```
6. Open a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any inquiries or feedback, please raise an issue here https://github.com/ipsimple/platform/issues
