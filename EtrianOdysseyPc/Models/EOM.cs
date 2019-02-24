using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Komponent.IO;

namespace EtrianOdysseyPc.Models
{
    public class EOM
    {
        private string _eomPath;
        private EOMFile _eomFile;

        public EOM(string eomPath)
        {
            if (!File.Exists(eomPath))
                throw new FileNotFoundException(eomPath);

            _eomPath = eomPath;
            _eomFile = new BinaryReaderX(File.OpenRead(eomPath)).ReadStruct<EOMFile>();

            SanityCheck();
        }

        private void SanityCheck()
        {
            if (_eomFile.magic != "EOM\0")
                throw new InvalidOperationException($"{_eomPath} isn't a valid EOM file");

            if (_eomFile.facets.Any(x => x.vertex1 >= _eomFile.vertexCount || x.vertex2 >= _eomFile.vertexCount || x.vertex3 >= _eomFile.vertexCount))
                throw new InvalidOperationException($"One facet points to a non-existing vertex");

            if (_eomFile.meshes.Any(x => x.facets.Any(y => y >= _eomFile.facetCount)))
                throw new InvalidOperationException($"One mesh points to a non-existing facet");
        }

        public GeometryModel3D RetrieveModel()
        {
            var mesh = new MeshGeometry3D();
            foreach (var vertex in _eomFile.vertices)
                mesh.Positions.Add(new Point3D(vertex.x, vertex.y, vertex.z));

            foreach (var facet in _eomFile.facets)
            {
                mesh.TriangleIndices.Add(facet.vertex1);
                mesh.TriangleIndices.Add(facet.vertex2);
                mesh.TriangleIndices.Add(facet.vertex3);
            }

            var model = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.YellowGreen));

            return model;
        }

        public string RetrieveName() => _eomFile.name;

        [Endianness(ByteOrder = ByteOrder.LittleEndian)]
        public class EOMFile
        {
            [FixedLength(4)]
            public string magic;
            public int vertexCount;
            public int facetCount;
            public int meshCount;
            public byte nameLength;
            [VariableLength("nameLength")]
            public string name;
            [VariableLength("vertexCount")]
            public Vertex[] vertices;
            [VariableLength("facetCount")]
            public Facet[] facets;
            [VariableLength("meshCount")]
            public Mesh[] meshes;
        }

        [Endianness(ByteOrder = ByteOrder.LittleEndian)]
        public class Vertex
        {
            public float x;
            public float y;
            public float z;
        }

        [Endianness(ByteOrder = ByteOrder.LittleEndian)]
        public class Facet
        {
            public int vertex1;
            public int vertex2;
            public int vertex3;
        }

        [Endianness(ByteOrder = ByteOrder.LittleEndian)]
        public class Mesh
        {
            public int facetCount;
            [VariableLength("facetCount")]
            public int[] facets;
        }
    }
}
