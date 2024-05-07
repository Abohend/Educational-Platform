using src.Data;
using src.Models;
using Microsoft.EntityFrameworkCore;
using src.Services;
using src.Models.Dto.Freelancer;

namespace src.Repository
{
	public class FreelancerRepository
	{
		private readonly Context _db;
		private readonly ImageService _imageService;

		public FreelancerRepository(Context db, ImageService imageService)
		{
			this._db = db;
			this._imageService = imageService;
		}

		public List<Freelancer>? Read()
		{
			return _db.Freelancers.ToList();
		}
		public Freelancer? Read(string id)
		{
			return _db.Freelancers.Find(id);
		}
		public void Update(string id, UpdateFreelancerDto freelancerDto, List<Skill>? skills)
		{
			Freelancer freelancer = Read(id)!;
			// update image
			if (freelancerDto.Image != null)
			{
				_imageService.DeleteImage(freelancer.ImagePath);
				freelancer.ImagePath = _imageService.UploadImage("freelancer", freelancerDto.Image);
			}

			//update skills
			if (skills != null)
			{
				foreach (var skill in skills)
				{
					if (!freelancer.Skills!.Contains(skill))
					{
						freelancer.Skills.Add(skill);
					}
				}
			}

			// Update other attributes
			freelancer.Name = freelancerDto.Name;
			freelancer.PhoneNumber = freelancerDto.PhoneNumber;
			if (freelancerDto.CategoryId != null)
				freelancer.CategoryId = freelancerDto.CategoryId.Value;

		}

		public bool Delete(string id)
		{
			var freelancer = Read(id);
			if (freelancer != null)
			{
				_db.Remove(freelancer);
				_db.SaveChanges();
				return true;
			}
			return false;
		}
	}
}
