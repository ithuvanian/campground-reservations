insert into park (name, state, establish_year, area, visitors, description)
	values ('Glacier National Park', 'Montana', 1910, 4101, 2338528, 'Glacier might very well be the most beautiful of America\''s national parks. John Muir called it "the best care-killing scenery on the continent." The mountains are steep, snowcapped, and punctuated by stunning mountain lakes and creeks. Much of the land remains wild and pristine, a result of its remote location and the lack of visitation in the 19th century.')


insert into park (name, state, establish_year, area, visitors, description)
	values ('Great Smoky Mountains National Park', 'Tennessee', 1934, 2114, 10099276, 'The Great Smoky Mountains are a mountain range rising along the Tennessee–North Carolina border in the southeastern United States. They are a subrange of the Appalachian Mountains, and form part of the Blue Ridge Physiographic Province. The range is sometimes called the Smoky Mountains and the name is commonly shortened to the Smokies. The Great Smokies are best known as the home of the Great Smoky Mountains National Park, which protects most of the range.')

insert into park (name, state, establish_year, area, visitors, description)
	values ('Everglades National Park', 'Florida', 1934, 6105, 1110901, 'There are no other Everglades in the world. They are, they have always been, one of the unique regions of the earth; remote, never wholly known. Nothing anywhere else is like them.')

insert into campground (name, open_from_mm, open_to_mm, daily_fee, park_id) 
	values ('Bowman Lake Campground', 3, 10, 55, 1)
insert into campground (name, open_from_mm, open_to_mm, daily_fee, park_id) 
	values ('Avalanche Campground', 5, 9, 45, 1)
insert into campground (name, open_from_mm, open_to_mm, daily_fee, park_id) 
	values ('Two Medicine Campground', 1, 12, 40, 1)
insert into campground (name, open_from_mm, open_to_mm, daily_fee, park_id) 
	values ('Balsam Mountain Campground', 1, 12, 40, 2)
insert into campground (name, open_from_mm, open_to_mm, daily_fee, park_id) 
	values ('Cades Cove Campground', 1, 12, 70, 2)
insert into campground (name, open_from_mm, open_to_mm, daily_fee, park_id) 
	values ('Lookrock Campground', 3, 11, 50, 2)
insert into campground (name, open_from_mm, open_to_mm, daily_fee, park_id) 
	values ('Long Pine Key Campground', 1, 12, 48, 3)
insert into campground (name, open_from_mm, open_to_mm, daily_fee, park_id) 
	values ('Flamingo Campground', 1, 12, 35, 3)

insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (1, 1, 4, 1, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (1, 2, 6, 0, 45, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (1, 3, 6, 0, 45, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (1, 4, 4, 1, 40, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (1, 5, 4, 1, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (2, 1, 8, 1, 50, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (2, 2, 6, 0, 45, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (2, 3, 6, 0, 45, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (2, 4, 4, 0, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (2, 5, 5, 0, 35, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (3, 1, 5, 1, 35, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (3, 2, 5, 0, 40, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (3, 3, 6, 1, 45, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (3, 4, 4, 1, 45, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (3, 5, 4, 1, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (4, 1, 4, 0, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (4, 2, 7, 0, 45, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (4, 3, 7, 0, 50, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (4, 4, 5, 0, 40, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (4, 5, 5, 1, 35, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (5, 1, 4, 1, 35, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (5, 2, 4, 1, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (5, 3, 4, 1, 45, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (5, 4, 2, 1, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (5, 5, 2, 1, 45, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (6, 1, 5, 1, 45, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (6, 2, 4, 0, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (6, 3, 6, 0, 50, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (6, 4, 6, 0, 50, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (6, 5, 4, 1, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (7, 1, 6, 0, 45, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (7, 2, 4, 1, 40, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (7, 3, 4, 0, 40, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (7, 4, 8, 0, 40, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (7, 5, 6, 0, 40, 0)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (8, 1, 4, 0, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (8, 2, 4, 1, 40, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (8, 3, 5, 1, 50, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (8, 4, 6, 1, 35, 1)
insert into site (campground_id, campground_site_number, max_occupancy, accessible, max_rv_length, utilities)
	values (8, 5, 5, 0, 40, 1)
