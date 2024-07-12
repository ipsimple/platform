```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3880/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11700K 3.60GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method     | Mean     | Error   | StdDev  |
|----------- |---------:|--------:|--------:|
| GetIpv4    | 109.0 μs | 1.98 μs | 3.57 μs |
| GetIpv4All | 108.3 μs | 2.09 μs | 3.44 μs |
| GetIpv6    | 107.8 μs | 2.15 μs | 3.41 μs |
| GetIpv6All | 108.2 μs | 2.15 μs | 2.80 μs |
