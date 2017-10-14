﻿using EquipmentStore.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace EquipmentStore.Core.Entities
{
    public class Machine
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string ShortDescription { get; set; }

		public string LongDescription { get; set; }

		public string CuttingFrequency { get; set; }

		public string MaximumPackageSize { get; set; }

		public string PackageMaterial { get; set; }

		public string MaxMoldingDepth { get; set; }

		public string Voltage { get; set; }

		public string Size { get; set; }

		public int Weight { get; set; }

        public EquipmentCategory Category { get; set; }

		[Required]
		public virtual MachineImage MainImage { get; set; }
	}
}
